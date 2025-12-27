using System;
using System.Collections.Generic;
using TaskApp.Items;


namespace TaskApp.Repository;

public class ItemRepository : IItemRepository
{
    private List<IItem> _items;
    public ItemRepository()
    {
        _items = new List<IItem>();
    }
    public IItem GetItemById(Guid itemId)
    {
        foreach (var item in _items)
        {
            if (item.Id == itemId)
            {
                return item;
            }
        }
        throw new Exception("Item not found");
    }
    public IItem GetItemByTitle(string title)
    {
        foreach (var item in _items)
        {
            if (item.Title.Equals(title))
            {
                return item;
            }
        }
        throw new Exception("Item not found");
    }
    public List<IItem> GetAllItemsForUser(User user)
    {
        var userList = new List<IItem>();
        foreach (var item in _items)
        {
            if (item.Owners.Contains(user))
            {
                userList.Add(item);
            }
        }
        return userList;
    }
    public void AddItem(IItem item)
    {
        if (_items.Contains(item))
        {
            throw new Exception("Item already exists");
        }
        _items.Add(item);
    }
    public void UpdateItem(IItem item)
    {
        if (!_items.Contains(item))
        {
            throw new Exception("Item does not exist");
        }
        _items.Remove(item);
        _items.Add(item); 
    }
    public void DeleteItem(IItem item)
    {
        if (!_items.Contains(item))
        {
            throw new Exception("Item does not exist");
        }
        _items.Remove(item);
    }
    public void ShareItem(Guid ownerId, Guid targetId, Guid itemId)
    {
        // Implementation for sharing an item can be added here if needed
        
    }
}