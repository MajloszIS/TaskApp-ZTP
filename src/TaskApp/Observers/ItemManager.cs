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
    private readonly ItemAccessProxy itemAccess;

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
    }
    public void UpdateItem(IItem item)
    {
        itemAccess.UpdateItem(item);
    }
    public void RemoveItem(IItem item)
    {
        itemAccess.DeleteItem(item);
    }
    public void ShareItem(User targetUser, IItem item)
    {
        itemAccess.ShareItem(targetUser, item);
    }
    public void SetCurrentUser(User? user)
    {
        itemAccess.SetCurrentUser(user);
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