using System;
using TaskApp.Repository;


public class Program
{
    static void Register(TaskAppFacade app)
    {
        Console.Write("= Register =\n");
        Console.Write("Username: ");
        var username = Console.ReadLine();

        Console.Write("Password: ");
        var password = Console.ReadLine();

        try
        {
            app.Register(username, password);
            Console.WriteLine("User registered successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static bool Login(TaskAppFacade app)
    {
        Console.Write("= Login =\n");
        Console.Write("Username: ");
        var username = Console.ReadLine();

        Console.Write("Password: ");
        var password = Console.ReadLine();

        try
        {
            if (app.Login(username, password))
            {
                Console.WriteLine("Login successful");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return false;
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
            Console.Write("Choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    //AddNote(app);
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
            Console.Write("Choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Register(app);
                    break;

                case "2":
                    if (Login(app))
                    {
                        UserMenu(app); // ← przejście do menu użytkownika
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
