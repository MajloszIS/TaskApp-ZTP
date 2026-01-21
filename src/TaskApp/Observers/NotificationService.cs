using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Observer;

public class NotificationService : IItemObserver
{
    public void Update(ItemChangeEvent evt)
    {
        Console.WriteLine($"[POWIADOMIENIE] Użytkownik {evt.User.Username} zmienił status zadania '{evt.Item.Title}' ({evt.ChangeType}).");
    }
}
