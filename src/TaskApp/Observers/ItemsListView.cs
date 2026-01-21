using System;
using System.Collections.Generic;
using System.Linq;
using TaskApp.Items;

namespace TaskApp.Observer;

public class ItemsListView : IItemObserver
{
    private Dictionary<string, List<string>> userLogs = new Dictionary<string, List<string>>();

    public void Update(ItemChangeEvent evt)
    {
        string username = evt.User.Username;
        string logEntry = $"[{DateTime.Now:HH:mm:ss}] {evt.ChangeType}: {evt.Item.Title}";

        if (!userLogs.ContainsKey(username))
        {
            userLogs[username] = new List<string>();
        }
        userLogs[username].Add(logEntry);

        var recentLogs = userLogs[username].Skip(Math.Max(0, userLogs[username].Count - 3)).ToList();

        Console.WriteLine($"\n--- [WIDOK LISTY] Ostatnie akcje użytkownika: {username} ---");

        foreach (var log in recentLogs)
        {
            Console.WriteLine(" > " + log);
        }

        Console.WriteLine("------------------------------------------------------");
        Console.WriteLine("Odświeżam widok tabeli...\n");
    }
}