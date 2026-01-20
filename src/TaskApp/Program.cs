using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TaskApp.Exceptions;
using TaskApp.Items;
using TaskApp.Observer;
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
        Console.WriteLine("= Pin Item =\n");
        Console.Write("Enter title of the item to pin: ");
        var title = Console.ReadLine();

        try
        {
            app.PinItem(title);
            Console.WriteLine("Item pinned successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Pause();
        }
    }

    static void ShareItem(TaskAppFacade app)
    {
        Console.WriteLine("= Share Item =\n");
        Console.Write("Enter title of the item to share: ");
        var title = Console.ReadLine();

        Console.Write("Enter username of the target user: ");
        var targetUser = Console.ReadLine();

        try
        {
            app.ShareItem(title, targetUser);
            Console.WriteLine($"Item shared with {targetUser} successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Pause();
        }
    }

    static void ShowItems(TaskAppFacade app)
    {
        Console.Clear();
        Console.WriteLine("=== YOUR ITEMS ===\n");
        try
        {
            var items = app.GetAllItems();

            if (items.Count == 0)
            {
                Console.WriteLine("(Empty)");
            }

            foreach (var item in items)
            {
                Console.WriteLine(FormatItemForDisplay(item));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Pause();
        }
        Pause();
    }
static void ViewFolder(TaskAppFacade app)
{
    var rootFolders = app.GetRootFolders();

    if (rootFolders.Count == 0)
    {
        Console.WriteLine("No folders available.");
        Pause();
        return;
    }

    Console.WriteLine("Available folders:");
    foreach (var f in rootFolders)
        Console.WriteLine($" - {f.Title}");

    Console.Write("\nEnter folder name: ");
    var name = Console.ReadLine();

    var folder = rootFolders.FirstOrDefault(f => f.Title == name);
    if (folder == null)
    {
        Console.WriteLine("Folder not found.");
        Pause();
        return;
    }

    Console.WriteLine($"\nFolder: {folder.Title}");
    if (folder.Children.Count == 0)
    {
        Console.WriteLine("(Empty)");
    }
    else
    {
        foreach (var item in folder.Children)
        {
            Console.WriteLine($" - {item.Title}");
        }
    }

    Pause();
}


static void CreateFolder(TaskAppFacade app)
{
    Console.Write("Folder title: ");
    var title = Console.ReadLine();

    try
    {
        app.CreateFolder(title);
        Console.WriteLine("Folder created.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    Pause();
}
static void AddItemToFolder(TaskAppFacade app)
{
    var allItems = app.GetAllItems();
    var folders = allItems.OfType<ItemGroup>().ToList();

    if (folders.Count == 0)
    {
        Console.WriteLine("No folders available.");
        Pause();
        return;
    }

    Console.WriteLine("Available folders:");
    DisplayFolders(folders);

    Console.Write("\nEnter the folder to add a file to: ");
    var folderName = Console.ReadLine();

    var folder = folders.FirstOrDefault(f => f.Title == folderName);
    if (folder == null)
    {
        Console.WriteLine("Folder not found.");
        Pause();
        return;
    }

    var availableFiles = allItems
        .Where(i => i is not ItemGroup && !folder.Children.Contains(i))
        .ToList();

    if (availableFiles.Count == 0)
    {
        Console.WriteLine($"No files available to add to folder '{folder.Title}'.");
        Pause();
        return;
    }

    Console.WriteLine($"\nFiles available to add to '{folder.Title}':");
    DisplayFolderContents(availableFiles);

    Console.Write("\nEnter file name to add: ");
    var fileName = Console.ReadLine();

    var file = availableFiles.FirstOrDefault(f => f.Title == fileName);
    if (file == null)
    {
        Console.WriteLine("File not found or already in folder.");
        Pause();
        return;
    }

    app.AddItemToFolder(folder.Id, file.Id);
    Console.WriteLine($"File '{file.Title}' added to folder '{folder.Title}'.");
    Pause();
}

static void DisplayFolders(List<ItemGroup> folders, int indent = 0)
{
    string indentStr = new string(' ', indent * 2);
    foreach (var folder in folders)
    {
        Console.WriteLine($"{indentStr}[Folder] {folder.Title} ({folder.Children.Count} items)");

        var subFolders = folder.Children.OfType<ItemGroup>().ToList();
        if (subFolders.Count > 0)
        {
            DisplayFolders(subFolders, indent + 1);
        }
    }
}

static void DisplayFolderContents(List<IItem> items, int indent = 0)
{
    string indentStr = new string(' ', indent * 2);
    foreach (var item in items)
    {
        if (item is ItemGroup folder)
        {
            Console.WriteLine($"{indentStr}[Folder] {folder.Title} ({folder.Children.Count} items)");
        }
        else
        {
            Console.WriteLine($"{indentStr}[Item] {item.Title}");
        }
    }
}




static void RemoveItemFromFolder(TaskAppFacade app)
{
    var folders = app.GetAllItems()
        .OfType<ItemGroup>()
        .ToList();

    if (folders.Count == 0)
    {
        Console.WriteLine("No folders available.");
        Pause();
        return;
    }

    Console.WriteLine("Folders:");
    foreach (var f in folders)
        Console.WriteLine($" - {f.Title}");

    Console.Write("\nFolder name: ");
    var folderName = Console.ReadLine();

    var folder = folders.FirstOrDefault(f => f.Title == folderName);
    if (folder == null)
    {
        Console.WriteLine("Folder not found.");
        Pause();
        return;
    }

    if (folder.Children.Count == 0)
    {
        Console.WriteLine("Folder is empty.");
        Pause();
        return;
    }

    Console.WriteLine("\nFiles in folder:");
    foreach (var i in folder.Children)
        Console.WriteLine($" - {i.Title}");

    Console.Write("\nFile name to remove: ");
    var fileName = Console.ReadLine();

    var file = folder.Children.FirstOrDefault(i => i.Title == fileName);
    if (file == null)
    {
        Console.WriteLine("File not found in folder.");
        Pause();
        return;
    }

    app.RemoveItemFromFolder(folder.Id, file.Id);

    Console.WriteLine("File removed from folder.");
    Pause();
}

static void ShareFolder(TaskAppFacade app)
{
    var folders = app.GetAllItems()
        .OfType<ItemGroup>()
        .ToList();

    Console.WriteLine("Folders:");
    foreach (var f in folders)
        Console.WriteLine($" - {f.Title}");

    Console.Write("\nFolder name: ");
    var name = Console.ReadLine();

    Console.Write("Target username: ");
    var username = Console.ReadLine();

    var folder = folders.FirstOrDefault(f => f.Title == name);
    if (folder == null)
    {
        Console.WriteLine("Folder not found.");
        Pause();
        return;
    }

    app.ShareFolder(folder.Id, username);
    Console.WriteLine("Folder shared.");
    Pause();
}

static void AddFolderToFolderConsole(TaskAppFacade app)
{
    var rootFolders = app.GetRootFolders();

    if (rootFolders.Count == 0)
    {
        Console.WriteLine("No folders available.");
        Pause();
        return;
    }

    Console.WriteLine("Available parent folders:");
    foreach (var f in rootFolders)
        Console.WriteLine($" - {f.Title}");

    Console.Write("\nParent folder name: ");
    var parent = Console.ReadLine();

    Console.Write("Which one to add?: ");
    var child = Console.ReadLine();

    try
    {
        app.AddFolderToFolder(parent, child);
        Console.WriteLine("Folder added successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
    Pause();
}



static void DeleteFolder(TaskAppFacade app)
{
    var rootFolders = app.GetRootFolders();

    if (rootFolders.Count == 0)
    {
        Console.WriteLine("No folders available.");
        Pause();
        return;
    }

    Console.WriteLine("Available folders to delete:");
    foreach (var f in rootFolders)
        Console.WriteLine($" - {f.Title}");

    Console.Write("\nFolder name to delete: ");
    var name = Console.ReadLine();

    try
    {
        app.DeleteFolder(name);
        Console.WriteLine("Folder deleted successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
    Pause();
}

    static void EditItem(TaskAppFacade app)
    {
        Console.WriteLine("= Edit Item =\n");
        Console.Write("Enter title of the item to edit: ");
        var title = Console.ReadLine();

        try
        {
            var itemToEdit = FindAndSelectItem(app, title);

            if (itemToEdit == null)
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
    static void DeleteItem(TaskAppFacade app)
    {
        Console.WriteLine("= Delete Item=\n");
        Console.Write("Enter title of the item to delete: ");
        var title = Console.ReadLine();

        try
        {
            var itemToDelete = FindAndSelectItem(app, title);

            if (itemToDelete == null) return;
            app.DeleteItem(itemToDelete.Title);

            Console.WriteLine("Item deleted successfully.");
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
        Console.WriteLine("7. Folder management");
        Console.WriteLine("8. Delete item");
        Console.WriteLine("0. Logout");
        Console.Write("Choice: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("1. Add Note");
                Console.WriteLine("2. Add Task");
                switch (Console.ReadLine())
                {
                    case "1": AddNote(app); break;
                    case "2": AddTask(app); break;
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

            case "7":
                while (true) 
                {
                    Console.Clear();
                    Console.WriteLine("=== Folder Management ===");
                    Console.WriteLine("1. Create folder");
                    Console.WriteLine("2. Add item to folder");
                    Console.WriteLine("3. Remove item from folder");
                    Console.WriteLine("4. Share folder");
                    Console.WriteLine("5. View folder");
                    Console.WriteLine("6. Delete folder");
                    Console.WriteLine("7. Add folder to folder");
                    Console.WriteLine("0. Back");

                    var folderChoice = Console.ReadLine();

                    switch (folderChoice)
                    {
                        case "1": CreateFolder(app); break;
                        case "2": AddItemToFolder(app); break;
                        case "3": RemoveItemFromFolder(app); break;
                        case "4": ShareFolder(app); break;
                        case "5": ViewFolder(app); break;
                        case "6": DeleteFolder(app); break;
                        case "7": AddFolderToFolderConsole(app); break;
                        case "0": goto ExitFolderManagement; 
                        default:
                        Console.WriteLine("Invalid option");
                        Pause();
                        break;
                    }
                }
                ExitFolderManagement:
                break;
            case "8":
                DeleteItem(app);
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