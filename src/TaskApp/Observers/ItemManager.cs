using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Observer;

public class ItemManager : IItemObservable
{
    private readonly List<IItemObserver> observers = new();
    private readonly IItemRepository repo;

    public ItemManager(IItemRepository repo)
    {
        this.repo = repo;
    }

    public void AddItem(User user, IItem item)
    {
        repo.Add(user.Id, item);
    }

    public void RemoveItem(User user, IItem item)
    {
        repo.Delete(user.Id, item);
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