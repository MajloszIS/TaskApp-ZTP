using System;

namespace TaskApp.Items;

public interface IItem
{
    Guid Id { get; }
    string Title { get; set; }
    DateTime CreatedAt { get; }
    List<User> Owners { get; set; }
    IItem Clone();
    void Restore(IItem state);
}