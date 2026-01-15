using System;
using System.Collections.Generic; // Potrzebne do List<>
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TaskApp.Exceptions;
using TaskApp.Items;
using TaskApp.Repository;

public class Program
{
    static void AddNote(TaskAppFacade app)
    {
        Console.WriteLine("= Add Note =\n");
        Console.WriteLine("Title: ");
        var title = Console.ReadLine();

        Console.WriteLine("Content: ");
        var content = Console.ReadLine();

        try
        {
            app.AddNote(title, content);
            Console.WriteLine("Note added successfully");
        }
        catch (TaskAppException ex)
        {
            Console.WriteLine(ex.Message);
            Pause();
        }
    }

    static void AddTask(TaskAppFacade app)
    {
        Console.WriteLine("= Add Task =\n");
        Console.Write("Title: ");
        var title = Console.ReadLine();

        Console.Write("Due date (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
        {
            dueDate = DateTime.Now.AddDays(1);
        }

        Console.Write("Priority: ");
        if (!int.TryParse(Console.ReadLine(), out int priority))
        {
            priority = 1;
        }

        try
        {
            app.AddTask(title, dueDate, priority);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Pause();
        }
    }

    static void PinItem(TaskAppFacade app)
    {
        var userrepo = new UserRepository();
        var itemrepo = new ItemRepository();
        var cosik = new TaskAppFacade(userrepo, itemrepo);

        var view = new ItemsListView();
        var notif = new NotificationService();

        cosik.AttachObserver(view);
        cosik.AttachObserver(notif);
        Console.WriteLine("Obserwatorzy podpięci\n");

        cosik.Register("alice", "password123");
        cosik.Login("alice", "password123");
        cosik.AddNote("Sample Note", "This is the content of the sample note.");
        cosik.AddTask("Sample Task", DateTime.Now.AddDays(7), 1);
        cosik.CreateFolder("MyFolder");
        cosik.AddItemToFolder("MyFolder", "Sample Note");


        var folder = cosik.GetAllItems().Find(i => i.Title == "MyFolder") as ItemGroup;

        if (folder != null)
        {
            Console.WriteLine($"Zawartość folderu '{folder.Title}':");
            foreach (var item in folder.GetChildren())
        {
            Console.WriteLine($"- {item.Title} ({item.GetType().Name})");
        }
}
else
{
    Console.WriteLine("Folder nie istnieje!");
}

        cosik.PinItem("Sample Note");
        var pinnedItems = cosik.GetAllItems();
        Console.WriteLine("After pin:");
        foreach (var it in pinnedItems)
        {
            Console.WriteLine($"Item: {it.Title}, Type: {it.GetType().Name}");
        }
        cosik.UnpinItem("Sample Note");
        var afterUnpin = cosik.GetAllItems();
        Console.WriteLine("After unpin:");
        foreach (var it in afterUnpin)
        {
            Console.WriteLine($"Item: {it.Title}, Type: {it.GetType().Name}");
        }
        cosik.Logout();

        cosik.Login("alice", "password123");
        cosik.AddNote("Another Note", "Content for another note.");
        cosik.Logout();

        cosik.Register("bob", "securepass");
        cosik.Login("bob", "securepass");
        cosik.AddTask("Bob's Task", DateTime.Now.AddDays(3), 2);
        cosik.ShareItem("Bob's Task", "alice");
        cosik.CloneItem("Bob's Task");
        var bobItems = cosik.GetAllItems();
        foreach (var item in bobItems)
        {
            string ownersList = "";
            int ownerzy = 0;
            foreach (var owner in item.Owners)
            {
                return;
            }

            Console.WriteLine($"Editing item: {FormatItemForDisplay(itemToEdit)}");
            Console.Write("New Title: ");
            var newTitle = Console.ReadLine();

            Console.Write("New Content (notes only): ");
            var newContent = Console.ReadLine();

            app.EditItem(itemToEdit.Id, newTitle, newContent);

            Console.WriteLine("Item updated successfully.");
        }
        catch (ItemNotFoundException)
        {
            Console.WriteLine($"Item with title '{title}' does not exist.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Pause();
    }

    static void CloneItem(TaskAppFacade app)
    {
        Console.WriteLine("= Clone Item =\n");
        Console.Write("Enter title of the item to clone: ");
        var title = Console.ReadLine();

        try
        {
            var itemToClone = FindAndSelectItem(app, title);

            if (itemToClone == null) return;
            app.CloneItem(itemToClone.Title);

            Console.WriteLine("Item cloned successfully.");
        }
        catch (ItemNotFoundException)
        {
            Console.WriteLine($"Item with title '{title}' does not exist.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Pause();
    }

    static IItem FindAndSelectItem(TaskAppFacade app, string title)
    {
        var allItems = app.GetAllItems();
        var matches = allItems.Where(i => i.Title == title).ToList();

        if (matches.Count == 0)
        {
            throw new ItemNotFoundException();
        }

        if (matches.Count == 1)
        {
            return matches[0];
        }
        Console.WriteLine($"\nFound {matches.Count} items with the title '{title}'. Select specific item:");

        for (int i = 0; i < matches.Count; i++)
        {
            var item = matches[i];
            Console.WriteLine($"{i + 1}. {FormatItemForDisplay(item)}");
        }

        while (true)
        {
            Console.Write("\nEnter number (or 0 to cancel): ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 0) return null;
                if (choice > 0 && choice <= matches.Count)
                {
                    return matches[choice - 1];
                }
            }
            Console.WriteLine("Invalid selection. Try again.");
        }
    }
    static string FormatItemForDisplay(IItem item)
    {
        string prefix = "";
        IItem displayItem = item;

        if (item is PinnedItemDecorator pinned)
        {
            prefix = "[PIN] ";
            displayItem = pinned.GetInnerItem();
        }

        if (displayItem is Tasky t)
        {
            string status = t.IsCompleted ? "[Completed]" : "[Not completed]";
            return $"{prefix}{status} {t.Title} (Due: {t.DueDate:yyyy-MM-dd}, Priority: {t.Priority})";
        }
        else if (displayItem is Note n)
        {
            return $"{prefix}[Note] {n.Title}: {n.Content}";
        }
        else if (displayItem is ItemGroup g)
        {
            return $"{prefix}[Folder] {g.Title} (Items: {g.Children.Count})";
        }
        else
        {
            return $"{prefix}{displayItem.Title}";
        }
    }

    static void Register(TaskAppFacade app)
    {
        Console.WriteLine("= Register =\n");
        Console.WriteLine("Username: ");
        var username = Console.ReadLine();

        Console.WriteLine("Password: ");
        var password = Console.ReadLine();

        try
        {
            app.Register(username, password);
            Console.WriteLine("User registered successfully");
        }
        catch (TaskAppException ex)
        {
            Console.WriteLine(ex.Message);
            Pause();
        }
    }

    static bool Login(TaskAppFacade app)
    {
        Console.WriteLine("= Login =\n");
        Console.WriteLine("Username: ");
        var username = Console.ReadLine();

        Console.WriteLine("Password: ");
        var password = Console.ReadLine();

        try
        {
            app.Login(username, password);
            Console.WriteLine("Login successful");
            return true;
        }
        catch (TaskAppException ex)
        {
            Console.WriteLine(ex.Message);
            Pause();
            return false;
        }
    }

    static void UserMenu(TaskAppFacade app)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== User Menu ===");
            Console.WriteLine("1. Add Note/Task");
            Console.WriteLine("2. Show my items");
            Console.WriteLine("3. Edit item");
            Console.WriteLine("4. Share item");
            Console.WriteLine("5. Pin item");
            Console.WriteLine("6. Clone item");
            Console.WriteLine("0. Logout");
            Console.WriteLine("Choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("1. Add Note");
                    Console.WriteLine("2. Add Task");
                    var choice1 = Console.ReadLine();
                    switch (choice1)
                    {
                        case "1":
                            AddNote(app);
                            break;
                        case "2":
                            AddTask(app);
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            Pause();
                            break;
                    }
                    break;
                case "2":
                    ShowItems(app);
                    break;
                case "3":
                    EditItem(app);
                    break;
                case "4":
                    ShareItem(app);
                    break;
                case "5":
                    PinItem(app);
                    break;
                case "6":
                    CloneItem(app);
                    break;
                case "0":
                    app.Logout();
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    Pause();
                    break;
            }
        }
    }

    static void Pause()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void Main()
    {
        var userRepo = new UserRepository();
        var itemRepo = new ItemRepository();
        var app = new TaskAppFacade(userRepo, itemRepo);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== TaskApp ===");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Register(app);
                    break;

                case "2":
                    if (Login(app))
                    {
                        UserMenu(app);
                    }
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("Invalid option");
                    Pause();
                    break;
            }
        }
    }
}