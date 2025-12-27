using System;
using System.Collections.Generic;
using TaskApp.Items;
using TaskApp.Repository;

namespace TaskApp.Access;

public class RealItemAccessService : IItemAccess
{
    private readonly IItemRepository itemRepo;
    private readonly IUserRepository userRepo;
    public RealItemAccessService(IItemRepository itemRepo, IUserRepository userRepo)
    {
        this.itemRepo = itemRepo;
        this.userRepo = userRepo;
    }
    public IItem GetItemById(Guid itemId)
    {
        return itemRepo.GetItemById(itemId);
    }
    public IItem GetItemByTitle(string title)
    {
        return itemRepo.GetItemByTitle(title);
    }
    public List<IItem> GetAllItemsForUser(User user)
    {
        return itemRepo.GetAllItemsForUser(user); 
    }
    public void AddItem(IItem item)
    {
        itemRepo.AddItem(item);
    }
    public void UpdateItem(IItem item)
    {
        itemRepo.UpdateItem(item);
    }
    public void DeleteItem(IItem item)
    {
        itemRepo.DeleteItem(item);
    }
    public void ShareItem(User user, User targetUser, IItem item)
    {
        if (!item.Owners.Contains(user))
        {
            throw new Exception("User does not own the item");
        }
        if (item.Owners.Contains(targetUser))
        {
            throw new Exception("Item already shared with target user");
        }
        item.Owners.Add(targetUser);
    }
    public void UnShareItem(User user, User targetUser, IItem item)
    {
        if (!item.Owners.Contains(user))
        {
            throw new Exception("User does not own the item");
        }
        if (item.Owners.Contains(targetUser))
        {
            throw new Exception("Item already shared with target user");
        }
        item.Owners.Remove(targetUser);
    }
}
