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
    public IItem GetItem(Guid userId, Guid itemId)
    {
        var item = itemRepo.GetById(itemId);
        return item;
    }
    public List<IItem> GetItemsForUser(Guid userId)
    {
        var items = itemRepo.GetAllForUser(userId);
        return items;
    }
    public void SaveItem(Guid userId, IItem item)
    {
        itemRepo.Update(userId, item);
    }
    public void ShareItem(Guid ownerId, Guid targetId, Guid itemId)
    {
        //to do: implement sharing logic
    }
}
