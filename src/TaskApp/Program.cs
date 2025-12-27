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

        cosik.Register("alice", "password123");
        cosik.Login("alice", "password123");
        cosik.AddNote("Sample Note", "This is the content of the sample note.");
        cosik.AddTask("Sample Task", DateTime.Now.AddDays(7), 1);
        cosik.Logout();

        cosik.Login("alice", "password123");
        cosik.AddNote("Another Note", "Content for another note.");
        var items = cosik.GetAllItems();
        foreach(var item in items)
        {
            Console.WriteLine($"Item: {item.Title}, Type: {item.GetType().Name}");
        }
        cosik.Login("alice", "password123");

        cosik.Register("bob", "securepass");
        cosik.Login("bob", "securepass");
        cosik.AddTask("Bob's Task", DateTime.Now.AddDays(3), 2);
        var bobItems = cosik.GetAllItems();
        foreach(var item in bobItems)
        {
            Console.WriteLine($"Item: {item.Title}, Type: {item.GetType().Name}");
        }
    }
}
