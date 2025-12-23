using System;
using System.Collections.Generic;
using TaskApp.Items;

namespace TaskApp.Repository;

public interface IItemRepository
{
    IItem GetById(Guid userId);
    List<IItem> GetAllForUser(Guid userId);
    void Add(Guid userId, IItem item);
    void Update(Guid userId, IItem item);
    void Delete(Guid userId, IItem item);
}
