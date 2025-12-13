using System;
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
        
    }
    public List<IItem> GetItemsForUser(User user)
    {
        
    }
    public void SaveItem(User user, IItem item)
    {
        
    }
    public void ShareItem(User owner, User target, IItem item)
    {
        
    }
}
