using System;
using TaskApp.Exceptions;
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
            Console.WriteLine("3. Show my items");
            Console.WriteLine("4. Logout");
            Console.WriteLine("Choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddNote(app);
                    break;

                case "2":
                    //AddTask(app);
                    break;

                case "3":
                    //ShowItems(app);
                    break;

                case "4":
                    app.Logout();
                    return; // wracamy do menu głównego

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
