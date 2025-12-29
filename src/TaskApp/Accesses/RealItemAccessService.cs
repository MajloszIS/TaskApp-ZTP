using System;
using System.Collections.Generic;
using TaskApp.Items;
using TaskApp.Repository;

namespace TaskApp.Access;

public class RealItemAccessService : IItemAccess
{
    private readonly IItemRepository itemRepo;
    public RealItemAccessService(IItemRepository itemRepo)
    {
        this.itemRepo = itemRepo;
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
    public void ShareItem(User targetUser, IItem item)
    {
        item.Owners.Add(targetUser);
    }
    public void UnShareItem(User targetUser, IItem item)
    {
        item.Owners.Remove(targetUser);
    }
}
