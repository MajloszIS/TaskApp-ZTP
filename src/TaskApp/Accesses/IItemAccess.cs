using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;

namespace TaskApp.Access;

public interface IItemAccess
{
    IItem GetItem(User user, Guid id);
    List<IItem> GetItemsForUser(User user);
    void SaveItem(User user, IItem item);
    void ShareItem(User owner, User target, IItem item);
}
