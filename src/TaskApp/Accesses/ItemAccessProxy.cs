using System;
using System.Collections.Generic;
using TaskApp.Items;

namespace TaskApp.Access;

public class ItemAccessProxy : IItemAccess
{
    private readonly IItemAccess innerService;
    private User? currentUser;

    public ItemAccessProxy(IItemAccess innerService)
    {
        this.innerService = innerService;
    }

    public void SetCurrentUser(User user)
    {
        currentUser = user;
    }
    public IItem GetItemById(Guid itemId)
    {
        var item = innerService.GetItemById(itemId);
    
        return item;
    }
    public IItem GetItemByTitle(string title)
    {
        var item = innerService.GetItemByTitle(title);
        return item;
    }
    public List<IItem> GetAllItemsForUser(User user)
    {
        var items = innerService.GetAllItemsForUser(user);
        return items;
    }
    public void AddItem(IItem item)
    {
        innerService.AddItem(item);
    }
    public void UpdateItem(IItem item)
    {
        innerService.UpdateItem(item);
    }
    public void DeleteItem(IItem item)
    {
        innerService.DeleteItem(item);
    }
    public void ShareItem(User user, User targetUser, IItem item)
    {
        innerService.ShareItem(user, targetUser, item);
    }
    public void UnShareItem(User user, User targetUser, IItem item)
    {
        innerService.UnShareItem(user, targetUser, item);
    }
}
