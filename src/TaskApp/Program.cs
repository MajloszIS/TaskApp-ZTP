using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

public class Program
{
    public static void Main()
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
        // Extra notes and tasks for Alice
        cosik.AddNote("Groceries", "Buy milk, eggs, bread.");
        cosik.AddNote("Ideas", "Build a note-taking app.");
        cosik.AddTask("Pay Bills", DateTime.Now.AddDays(1), 3);
        cosik.AddTask("Workout", DateTime.Now.AddHours(6), 2);
        cosik.AddTask("Read Book", DateTime.Now.AddDays(5), 1);
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

        cosik.AddTask("Clean House", DateTime.Now.AddDays(2), 2);
        cosik.AddNote("Project Plan", "Outline tasks and milestones.");
        cosik.Logout();

        cosik.Register("bob", "securepass");
        cosik.Login("bob", "securepass");
        cosik.AddTask("Bob's Task", DateTime.Now.AddDays(3), 2);

        cosik.AddTask("Bob's Errand", DateTime.Now.AddDays(4), 1);
        cosik.AddNote("Bob's Note", "Call Alice about the report.");
        cosik.ShareItem("Bob's Task", "alice");
        cosik.CloneItem("Bob's Task");
        var bobItems = cosik.GetAllItems();
        foreach (var item in bobItems)
        {
            string ownersList = "";
            int ownerzy = 0;
            foreach (var owner in item.Owners)
            {
                if (ownerzy > 0)
                {
                    ownersList += ", ";
                }
                ownersList += owner.Username;
                ownerzy++;
            }
            Console.WriteLine($"Item: {item.Title}, Type: {item.GetType().Name}, owners: {ownersList}");
        }
        cosik.Logout();
        cosik.Login("alice", "password123");
        cosik.CloneItem("Sample Note");
        var items = cosik.GetAllItems();
        foreach (var item in items)
        {
            Console.WriteLine($"Item: {item.Title}, Type: {item.GetType().Name}");
        }
        cosik.DeleteItem("Bob's Task");

    }
}
