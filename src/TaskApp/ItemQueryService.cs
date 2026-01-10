using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;

public class ItemQueryService
{
    private readonly IItemRepository repo;

    public ItemQueryService(IItemRepository repo)
    {
        this.repo = repo;
    }

    public List<IItem> Filter(User user, string criteria)
    {
        var items = repo.GetAllItemsForUser(user);
        if (string.IsNullOrWhiteSpace(criteria)) return items;

        var tokens = criteria.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                             .Select(t => t.ToLowerInvariant())
                             .ToList();

        IEnumerable<IItem> query = items;

        var now = DateTime.Now;
        var startOfWeek = now.Date.AddDays(-((7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7));
        var endOfWeek = startOfWeek.AddDays(6);

        foreach (var token in tokens)
        {
            if (token is "notes" or "type:notes")
            {
                query = query.Where(i => i is Note);
            }
            else if (token is "tasks" or "type:tasks")
            {
                query = query.Where(i => i is Tasky);
            }
            else if (token.StartsWith("completed:"))
            {
                var val = token.Substring("completed:".Length);
                if (bool.TryParse(val, out var completed))
                {
                    query = query.Where(i => i is Tasky t && t.IsCompleted == completed);
                }
            }
            else if (token.StartsWith("priority:"))
            {
                var expr = token.Substring("priority:".Length).Trim();
                query = query.Where(i => i is Tasky);

                if (expr.StartsWith(">=") && int.TryParse(expr.Substring(2), out var ge))
                {
                    query = query.Where(i => i is Tasky t && t.Priority >= ge);
                }
                else if (expr.StartsWith("<=") && int.TryParse(expr.Substring(2), out var le))
                {
                    query = query.Where(i => i is Tasky t && t.Priority <= le);
                }
                else if (expr.StartsWith("=") && int.TryParse(expr.Substring(1), out var eq))
                {
                    query = query.Where(i => i is Tasky t && t.Priority == eq);
                }
                else if (int.TryParse(expr, out var exact))
                {
                    query = query.Where(i => i is Tasky t && t.Priority == exact);
                }
            }
            else if (token.StartsWith("due:"))
            {
                var val = token.Substring("due:".Length).Trim();
                query = query.Where(i => i is Tasky);
                switch (val)
                {
                    case "overdue":
                        query = query.Where(i => i is Tasky t && t.DueDate < now && !t.IsCompleted);
                        break;
                    case "today":
                        query = query.Where(i => i is Tasky t && t.DueDate.Date == now.Date);
                        break;
                    case "week":
                        query = query.Where(i => i is Tasky t && t.DueDate.Date >= startOfWeek && t.DueDate.Date <= endOfWeek);
                        break;
                    case "month":
                        query = query.Where(i => i is Tasky t && t.DueDate.Month == now.Month && t.DueDate.Year == now.Year);
                        break;
                }
            }
            else if (token.StartsWith("tag:"))
            {
                var tag = token.Substring("tag:".Length);
                query = query.Where(i => i is Note n && n.Tags != null && n.Tags.Any(t => string.Equals(t, tag, StringComparison.OrdinalIgnoreCase)));
            }
        }

        return query.ToList();
    }

    public List<IItem> Sort(List<IItem> items, string mode)
    {
        if (items == null || items.Count == 0) return new List<IItem>();
        var m = (mode ?? "").ToLowerInvariant();

        IEnumerable<IItem> ordered;

        switch (m)
        {
            case "title-asc":
                ordered = items.OrderBy(i => i.Title, StringComparer.OrdinalIgnoreCase);
                break;
            case "title-desc":
                ordered = items.OrderByDescending(i => i.Title, StringComparer.OrdinalIgnoreCase);
                break;
            case "created-asc":
                ordered = items.OrderBy(i => i.CreatedAt);
                break;
            case "created-desc":
                ordered = items.OrderByDescending(i => i.CreatedAt);
                break;
            case "due-asc":
                ordered = items
                    .OrderBy(i => i is Tasky ? 0 : 1)
                    .ThenBy(i => (i as Tasky)?.DueDate);
                break;
            case "due-desc":
                ordered = items
                    .OrderBy(i => i is Tasky ? 0 : 1)
                    .ThenByDescending(i => (i as Tasky)?.DueDate);
                break;
            case "priority-asc":
                ordered = items
                    .OrderBy(i => i is Tasky ? 0 : 1)
                    .ThenBy(i => (i as Tasky)?.Priority ?? int.MaxValue);
                break;
            case "priority-desc":
                ordered = items
                    .OrderBy(i => i is Tasky ? 0 : 1)
                    .ThenByDescending(i => (i as Tasky)?.Priority ?? int.MinValue);
                break;
            case "completed-first":
                ordered = items
                    .OrderBy(i => i is Tasky ? 0 : 1)
                    .ThenBy(i => i is Tasky t && t.IsCompleted ? 0 : 1);
                break;
            case "completed-last":
                ordered = items
                    .OrderBy(i => i is Tasky ? 0 : 1)
                    .ThenBy(i => i is Tasky t && t.IsCompleted ? 1 : 0);
                break;
            default:
                ordered = items.OrderByDescending(i => i.CreatedAt);
                break;
        }

        return ordered.ToList();
    }

    public List<IItem> Search(User user, string text)
    {
        var items = repo.GetAllItemsForUser(user);
        if (string.IsNullOrWhiteSpace(text)) return items;

        var needle = text.Trim();

        var result = items.Where(i =>
            i.Title != null && i.Title.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0
        ).ToList();

        return result;
    }

    public void PrintAllItems(User user)
    {
        var items = repo.GetAllItemsForUser(user);
        var ordered = items
            .OrderBy(i => i is PinnedItemDecorator pd && pd.GetInnerItem() is Note ? 0 : 1);

        foreach (var i in ordered)
        {
            var baseItem = i is ItemDecorator d ? d.GetInnerItem() : i;
            if (baseItem is Tasky t)
            {
                Console.WriteLine($"{baseItem.Title} {t.DueDate:yyyy-MM-dd HH:mm} p{t.Priority}");
            }
            else
            {
                Console.WriteLine(baseItem.Title);
            }
        }
    }

    public void PrintFilteredItems(User user, string criteria)
    {
        var filtered = Filter(user, criteria);
        foreach (var i in filtered)
        {
            if (i is Tasky t)
            {
                Console.WriteLine($"{i.Title} {t.DueDate:yyyy-MM-dd HH:mm} p{t.Priority}");
            }
            else
            {
                Console.WriteLine(i.Title);
            }
        }
    }

    public void PrintSortedItems(List<IItem> items, string mode)
    {
        var sorted = Sort(items, mode);
        foreach (var i in sorted)
        {
            if (i is Tasky t)
            {
                Console.WriteLine($"{i.Title} {t.DueDate:yyyy-MM-dd HH:mm} p{t.Priority}");
            }
            else
            {
                Console.WriteLine(i.Title);
            }
        }
    }

    public void PrintSearchedItems(User user, string text)
    {
        var found = Search(user, text);
        foreach (var i in found)
        {
            Console.WriteLine(i.Title);
        }
    }
}
