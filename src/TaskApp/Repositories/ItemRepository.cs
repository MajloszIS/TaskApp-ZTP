using System;
using System.Collections.Generic;
using TaskApp.Items;


namespace TaskApp.Repository;

public class ItemRepository : IItemRepository
{
    private Dictionary<Guid, List<IItem>> itemsByUser;
    public ItemRepository()
    {
        itemsByUser = new Dictionary<Guid, List<IItem>>();
    }
    public IItem GetById(Guid userId)
    {
        foreach (var userItems in itemsByUser.Values)
        {
            foreach (var item in userItems)
            {
                if (item.Id == userId)
                {
                    return item;
                }
            }
        }
        throw new Exception("Item not found");
    }
    public List<IItem> GetAllForUser(Guid userId)
    {
        var userList = itemsByUser[userId];
        return userList;
    }
    public void Add(Guid userId, IItem item)
    {
        if (!itemsByUser.ContainsKey(userId))
        {
            itemsByUser[userId] = new List<IItem>();
        }
        itemsByUser[userId].Add(item);
    }
    public void Update(Guid userId, IItem item)
    {
        if (!itemsByUser.ContainsKey(userId))
        {
            throw new Exception("User has no items");
        }
        itemsByUser[userId].Remove(item);
        itemsByUser[userId].Add(item);
    }
    public void Delete(Guid userId, IItem item)
    {
        if (!itemsByUser.ContainsKey(userId))
        {
            throw new Exception("User has no items");
        }
        itemsByUser[userId].Remove(item);
    }
}