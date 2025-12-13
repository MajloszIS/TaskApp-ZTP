using System;
public interface IItemAccess
{
    IItem GetItem(User user, Guid id);
    List<IItem> GetItemsForUser(User user);
    void SaveItem(User user, IItem item);
    void ShareItem(User owner, User target, IItem item);
}
