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

    private void EnsureLoggedInAndOwner(IItem item)
    {
        if (currentUser == null)
            throw new Exception("No current user set");
        if (!item.Owners.Contains(currentUser))
            throw new Exception("Access denied to item");
    }
    public void SetCurrentUser(User? user)
    {
        currentUser = user;
    }
    public IItem GetItemById(Guid itemId)
    {
        var item = innerService.GetItemById(itemId);
        EnsureLoggedInAndOwner(item);
        return item;
    }
    public IItem GetItemByTitle(string title)
    {
        var item = innerService.GetItemByTitle(title);
        EnsureLoggedInAndOwner(item);
        return item;
    }
    public List<IItem> GetAllItemsForUser(User user)
    {
        var items = innerService.GetAllItemsForUser(user);
        foreach (var item in items)
        {
            if (!item.Owners.Contains(user))
            {
                throw new Exception("Access denied to item");
            }
        }
        return items;
    }
    public void AddItem(IItem item)
    {
        EnsureLoggedInAndOwner(item);
        item.Owners.Add(currentUser);
        innerService.AddItem(item);
    }
    public void UpdateItem(IItem item)
    {
        EnsureLoggedInAndOwner(item);
        innerService.UpdateItem(item);
    }
    public void DeleteItem(IItem item)
    {
        EnsureLoggedInAndOwner(item);
        innerService.DeleteItem(item);
    }
    public void ShareItem(User targetUser, IItem item)
    {
        EnsureLoggedInAndOwner(item);
        if (targetUser == currentUser)
        {
            throw new Exception("Owner cannot share the item with themselves");
        }
        if (item.Owners.Contains(targetUser))
        {
            throw new Exception("Item is already shared with the target user");
        }
        innerService.ShareItem(targetUser, item);
    }
    public void UnShareItem(User targetUser, IItem item)
    {
        EnsureLoggedInAndOwner(item);
        if (targetUser == currentUser)
        {
            throw new Exception("Owner cannot unshare the item from themselves");
        }
        if (!item.Owners.Contains(targetUser))
        {
            throw new Exception("Item is not shared with the target user");
        }
        innerService.UnShareItem(targetUser, item);
    }
}
