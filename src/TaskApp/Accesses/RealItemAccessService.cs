using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;

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
    public IItem GetItem(User user, Guid id)
    {
        var list = new Note();
        return list;
    }
    public List<IItem> GetItemsForUser(User user)
    {
        var list = new List<IItem>();
        return list;
    }
    public void SaveItem(User user, IItem item)
    {
        
    }
    public void ShareItem(User owner, User target, IItem item)
    {
        
    }
}
