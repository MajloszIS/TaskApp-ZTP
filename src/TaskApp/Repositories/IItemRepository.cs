using System;
using System.Collections.Generic;
using TaskApp.Items;

namespace TaskApp.Repository;

public interface IItemRepository
{
    IItem GetById(Guid userId);
    IItem GetByTitle(string title);
    List<IItem> GetAllForUser(Guid userId);
    void Add(Guid userId, IItem item);
    void Update(Guid userId, IItem item);
    void Delete(Guid userId, IItem item);
}
