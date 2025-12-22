using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;

public class ItemQueryService
{
    private readonly IItemRepository repo;
    public ItemQueryService(IItemRepository repo)
    {
        this.repo = repo;
    }
    public List<IItem> Filter(User user, string criteria)
    {
        var list = new List<IItem>();
        return list;
    }
    public List<IItem> Sort(List<IItem> items, string mode)
    {
        var list = new List<IItem>();
        return list;
    }
    public List<IItem> Search(User user, string text)
    {
        var list = new List<IItem>();
        return list;
    }
}
