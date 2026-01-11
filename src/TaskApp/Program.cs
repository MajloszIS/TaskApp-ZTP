using System;
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



    static void CreateFolder(TaskAppFacade app)
{
    Console.WriteLine("= Create Folder =");
    Console.Write("Folder title: ");
    var title = Console.ReadLine();

    try
    {
        app.AddFolder(title);
        Console.WriteLine("Folder created successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    Pause();
}


static void AddItemToFolder(TaskAppFacade app)
{
    try
    {
        var items = app.GetAllItems();

        Console.WriteLine("=== Folders ===");
        foreach (var item in items)
        {
            PrintFolderNames(item, 0);
        }

        Console.Write("\nFolder name: ");
        var folderName = Console.ReadLine();

        Console.WriteLine("\n=== Files ===");
        foreach (var item in items)
        {
            PrintFileNames(item);
        }

        Console.Write("\nFile name to move: ");
        var itemName = Console.ReadLine();

        app.MoveItemToFolder(itemName, folderName);
        Console.WriteLine("Item moved to folder");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    Pause();
}



static void PrintFolderNames(IItem item, int indent)
{
    if (item is ItemGroup group)
    {
        Console.WriteLine($"{new string(' ', indent * 2)}- {group.Title}");
        foreach (var child in group.Children)
        {
            PrintFolderNames(child, indent + 1);
        }
    }
}

static void PrintFileNames(IItem item)
{
    if (item is not ItemGroup)
    {
        Console.WriteLine($"- {item.Title}");
    }

    if (item is ItemGroup group)
    {
        foreach (var child in group.Children)
        {
            PrintFileNames(child);
        }
    }
}


static void PrintFoldersOnly(IItem item, int indent)
{
    if (item is ItemGroup group)
    {
        Console.WriteLine($"{new string(' ', indent * 2)}- {group.Title}");
        foreach (var child in group.Children)
        {
            PrintFoldersOnly(child, indent + 1);
        }
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
        Console.WriteLine("1. Add note");
        Console.WriteLine("2. Add task");
        Console.WriteLine("3. Create folder");
        Console.WriteLine("4. Add item to folder");
        Console.WriteLine("5. Show my items");
        Console.WriteLine("6. Logout");
        Console.WriteLine("Choice: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddNote(app);
                break;

            case "2":
            // AddTask(app);
                break;

            case "3":
                CreateFolder(app);
                break;

            case "4":
                AddItemToFolder(app);
                break;

            case "5":
             //   ShowItems(app);
                break;

            case "6":
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
