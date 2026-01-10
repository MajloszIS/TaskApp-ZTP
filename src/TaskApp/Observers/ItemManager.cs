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
    private readonly List<IItemObserver> observers = new List<IItemObserver>();
    private readonly ItemAccessProxy itemAccess;
    private User? currentUser;

    public ItemManager(IItemRepository itemRepo)
    {
        itemAccess = new ItemAccessProxy(new RealItemAccessService(itemRepo));
    }

    public IItem? GetItemById(Guid itemId)
    {
        return itemAccess.GetItemById(itemId);
    }
    public IItem? GetItemByTitle(string title)
    {
        return itemAccess.GetItemByTitle(title);
    }
    public List<IItem> GetAllItemsForUser(User user)
    {
        return itemAccess.GetAllItemsForUser(user);
    }
    public void AddItem(IItem item)
    {
        itemAccess.AddItem(item);
        var user = currentUser ?? new User("System", "");
        Notify(new ItemChangeEvent("DODANO", item, user));
    }
    public void UpdateItem(IItem item)
    {
        itemAccess.UpdateItem(item);

        var user = currentUser ?? new User("System", "");
        Notify(new ItemChangeEvent("ZAKTUALIZOWANO", item, user));
    }
    public void RemoveItem(IItem item)
    {
        itemAccess.DeleteItem(item);

        var user = currentUser ?? new User("System", "");
        Notify(new ItemChangeEvent("USUNIĘTO", item, user));
    }
    public void ShareItem(User targetUser, IItem item)
    {
        itemAccess.ShareItem(targetUser, item);
    }
    public void SetCurrentUser(User? user)
    {
        currentUser = user;
        itemAccess.SetCurrentUser(user);
    }
    public void Attach(IItemObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IItemObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(ItemChangeEvent evt)
    {
        foreach (var observer in observers)
        {
            observer.Update(evt);
        }
    }

    public void UnShareItem(User target, IItem item)
    {
        itemAccess.UnShareItem(target, item);
    }
}