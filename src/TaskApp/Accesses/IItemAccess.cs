using System;
using System.Collections.Generic;
using TaskApp.Items;

namespace TaskApp.Access;

public interface IItemAccess
{
    IItem GetItem(Guid userId, Guid itemId);
    IItem GetItemByTitle(string title);
    List<IItem> GetItemsForUser(Guid userId);
    void SaveItem(Guid userId, IItem item);
    void DeleteItem(Guid userId, IItem item);
    void ShareItem(Guid ownerId, Guid targetId, Guid itemId);
}
