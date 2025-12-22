using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;


public class ItemRepository : IItemRepository
{
    private Dictionary<Guid, List<IItem>> itemsByUser;
    public ItemRepository()
    {
        itemsByUser = new Dictionary<Guid, List<IItem>>();
    }
    public IItem GetById(Guid id)
    {
        var list = new Note();
        return list;
    }
    public List<IItem> GetAllForUser(User user)
    {
        var list = new List<IItem>();
        return list;
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