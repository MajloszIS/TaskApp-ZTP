using System;
public interface IItemRepository
{
    IItem GetById(Guid id);
    List<IItem> GetAllForUser(User user);
    void Add(User user, IItem item);
    void Update(User user, IItem item);
    void Delete(User user, IItem item);
}
