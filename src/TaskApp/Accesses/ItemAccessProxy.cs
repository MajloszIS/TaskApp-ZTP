using System;
using System.Collections.Generic;
using TaskApp.Exceptions;
using TaskApp.Items;

namespace TaskApp.Access;

public class ItemAccessProxy : IItemAccess
{
    private readonly IItemAccess innerService;
    private List<IItem> cachedItems = new List<IItem>();
    private User? currentUser;

    public ItemAccessProxy(IItemAccess innerService)
    {
        this.innerService = innerService;
    }

    private void EnsureLoggedInAndOwner(IItem item)
    {
        if (currentUser == null)
        {
            throw new AccessDeniedException();
        }
        if (!item.Owners.Contains(currentUser))
        {
            cachedItems.Clear();
            throw new AccessDeniedException();
        }
    }
    public void SetCurrentUser(User? user)
    {
        if (user == null)
        {
            cachedItems.Clear();
        }
        currentUser = user;
    }
    public IItem GetItemById(Guid itemId)
    {
        if (cachedItems.Exists(i => i.Id == itemId))
        {
            var cachedItem = cachedItems.Find(i => i.Id == itemId)!;
            EnsureLoggedInAndOwner(cachedItem);
            return cachedItem;
        }
        else
        {
            var item = innerService.GetItemById(itemId);
            EnsureLoggedInAndOwner(item);
            cachedItems.Add(item);
            return item;
        }
    }
    public IItem GetItemByTitle(string title)
    {
        if (cachedItems.Exists(i => i.Title == title))
        {
            var cachedItem = cachedItems.Find(i => i.Title == title)!;
            EnsureLoggedInAndOwner(cachedItem);
            return cachedItem;
        }
        else
        {
            var item = innerService.GetItemByTitle(title);
            EnsureLoggedInAndOwner(item);
            cachedItems.Add(item);
            return item;
        }
    }
    public List<IItem> GetAllItemsForUser(User user)
    {
        if (currentUser == null)
            throw new AccessDeniedException();

        if (user.Id != currentUser.Id)
            throw new AccessDeniedException();
            
        var items = innerService.GetAllItemsForUser(user);
        foreach (var item in items)
        {
            if (!item.Owners.Contains(user))
            {
                cachedItems.Clear();
                throw new AccessDeniedException();
            }
        }
        return items;
    }
    public void AddItem(IItem item)
    {
        if (currentUser == null)
        {
            throw new AccessDeniedException();
        }
        if (!item.Owners.Contains(currentUser))
        {
            item.Owners.Add(currentUser);
        }
        innerService.AddItem(item);
    }
    public void UpdateItem(IItem item)
    {
        EnsureLoggedInAndOwner(item);
        if (cachedItems.Exists(i => i.Id == item.Id))
        {
            cachedItems.RemoveAll(i => i.Id == item.Id);
        }
        innerService.UpdateItem(item);
    }
    public void DeleteItem(IItem item)
    {
        EnsureLoggedInAndOwner(item);
        if (cachedItems.Exists(i => i.Id == item.Id))
        {
            cachedItems.RemoveAll(i => i.Id == item.Id);
        }
        innerService.DeleteItem(item);
    }
    public void ShareItem(User targetUser, IItem item)
    {
        EnsureLoggedInAndOwner(item);
        if (targetUser == currentUser)
        {
            throw new ValidationException("Owner cannot share the item with themselves.");
        }
        if (item.Owners.Contains(targetUser))
        {
            throw new ValidationException("Item is already shared with the target user.");
        }
        if (cachedItems.Exists(i => i.Id == item.Id))
        {
                cachedItems.RemoveAll(i => i.Id == item.Id);
        }
        innerService.ShareItem(targetUser, item);
    }
    public void UnShareItem(User targetUser, IItem item)
    {
        EnsureLoggedInAndOwner(item);
        if (targetUser == currentUser)
        {
            throw new ValidationException("Owner cannot unshare the item with themselves.");
        }
        if (!item.Owners.Contains(targetUser))
        {
            throw new ValidationException("Item is not shared with the target user.");
        }
        if (cachedItems.Exists(i => i.Id == item.Id))
        {
            cachedItems.RemoveAll(i => i.Id == item.Id);
        }
        innerService.UnShareItem(targetUser, item);
    }
}
