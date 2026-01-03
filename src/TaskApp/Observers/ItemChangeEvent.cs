using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Observer;

public class ItemChangeEvent
{
    public string ChangeType { get; }
    public IItem Item { get; }
    public User User { get; }

    public ItemChangeEvent(string changeType, IItem item, User user)
    {
        ChangeType = changeType;
        Item = item;
        User = user;
    }
}