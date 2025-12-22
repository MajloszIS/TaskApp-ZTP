using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;

namespace TaskApp.Observer;

public class ItemManager : IItemObservable
{
    private readonly List<IItemObserver> observers = new();
    private readonly IItemRepository repo;

    public ItemManager(IItemRepository repo)
    {

    }

    public void AddItem(User user, IItem item)
    {

    }

    public void RemoveItem(User user, IItem item)
    {

    }

    public void UpdateItem(User user, IItem item)
    {

    }

    public List<IItem> GetAllItems(User user)
    {
        var list = new List<IItem>();
        return list;
    }

    public void Attach(IItemObserver observer)
    {

    }

    public void Detach(IItemObserver observer)
    {

    }

    public void Notify(ItemChangeEvent evt)
    {

    }
}