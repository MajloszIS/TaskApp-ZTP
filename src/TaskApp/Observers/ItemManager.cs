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
    private readonly IItemAccess itemAccess;
    private readonly IItemRepository repo;

    public ItemManager(IItemRepository repo)
    {
        this.repo = repo;
        itemAccess = new ItemAccessProxy(new RealItemAccessService(repo));
    }

    public void AddItem(User user, IItem item)
    {
        itemAccess.SaveItem(user.Id, item);
    }

    public void RemoveItem(User user, IItem item)
    {
        itemAccess.DeleteItem(user.Id, item);
    }

    public void UpdateItem(User user, IItem item)
    {

    }
    public void ShareItem(User owner, User target, IItem item)
    {
        itemAccess.ShareItem(owner.Id, target.Id, item.Id);
    }
    public IItem? GetItem(Guid userId,  Guid itemId)
    {
        return itemAccess.GetItem(userId, itemId);
    }

    public List<IItem> GetAllItems(User user)
    {
        var list = itemAccess.GetItemsForUser(user.Id);
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