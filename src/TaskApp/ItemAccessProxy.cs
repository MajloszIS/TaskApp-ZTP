using System;
public class ItemAccessProxy : IItemAccess
{
    private readonly IItemAccess innerService;
    private readonly User currentUser;
    public ItemAccessProxy(IItemAccess innerService)
    {
        this.innerService = innerService;
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
