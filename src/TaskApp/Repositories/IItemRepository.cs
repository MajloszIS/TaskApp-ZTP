using System;
using System.Collections.Generic;
using TaskApp.Items;

namespace TaskApp.Repository;

public interface IItemRepository
{
    IItem GetItemById(Guid itemId);
    IItem GetItemByTitle(string title);
    List<IItem> GetAllItemsForUser(User user);
    void AddItem(IItem item);
    void UpdateItem(IItem item);
    void DeleteItem(IItem item);
}
