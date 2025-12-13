public class ItemRepository : IItemRepository
{
    private Dictionary<Guid, List<IItem>> itemsByUser;
    public ItemRepository()
    {
        itemsByUser = new Dictionary<Guid, List<IItem>>();
    }
    public IItem GetById(Guid id)
    {
        
    }
    public List<IItem> GetAllForUser(User user)
    {
        
    }
    public void Add(User user, IItem item)
    {
        
    }
    public void Update(User user, IItem item)
    {
        
    }
    public void Delete(User user, IItem item)
    {
        
    }
}