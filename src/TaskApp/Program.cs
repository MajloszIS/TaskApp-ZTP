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
