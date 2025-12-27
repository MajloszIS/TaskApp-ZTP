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
        
    }
}
