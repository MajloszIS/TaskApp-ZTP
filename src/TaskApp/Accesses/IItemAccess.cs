using System;
using System.Collections.Generic;
using TaskApp.Items;

namespace TaskApp.Access;

public interface IItemAccess
{
    IItem GetItemById(Guid itemId);
    IItem GetItemByTitle(string title);
    List<IItem> GetAllItemsForUser(User user);
    void AddItem(IItem item);
    void UpdateItem(IItem item);
    void DeleteItem(IItem item);
    void ShareItem(User user, User targetUser, IItem item);
    void UnShareItem(User user, User targetUser, IItem item);
}
