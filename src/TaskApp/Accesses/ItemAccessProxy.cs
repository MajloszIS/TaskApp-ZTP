using System;
using System.Collections.Generic;
using TaskApp.Items;

namespace TaskApp.Access;

public class ItemAccessProxy : IItemAccess
{
    private readonly IItemAccess innerService;
    private User? currentUser { set; get; }
    public ItemAccessProxy(IItemAccess innerService)
    {
        this.innerService = innerService;
    }
    public IItem GetItem(Guid userId, Guid itemId)
    {
        if(userId != currentUser.Id)
        {
            throw new Exception("Cannot access items of another user");
        }
        var item = innerService.GetItem(userId, itemId);
        return item;
    }
    public List<IItem> GetItemsForUser(Guid userId)
    {
        if(userId != currentUser.Id)
        {
            throw new Exception("Cannot access items of another user");
        }
        var items = innerService.GetItemsForUser(userId);
        return items;
    }
    public void SaveItem(Guid userId, IItem item)
    {
        if(userId != currentUser.Id)
        {
            throw new Exception("Cannot save item for another user");
        }
        innerService.SaveItem(userId, item);
    }
    public void DeleteItem(Guid userId, IItem item)
    {
        if(userId != currentUser.Id)
        {
            throw new Exception("Cannot delete item for another user");
        }
        innerService.DeleteItem(userId, item);
    }
    public void ShareItem(Guid ownerId, Guid targetId, Guid itemId)
    {
        if(ownerId != currentUser.Id)
        {
            throw new Exception("Only the owner can share the item");
        }
        innerService.ShareItem(ownerId, targetId, itemId);
    }
}
