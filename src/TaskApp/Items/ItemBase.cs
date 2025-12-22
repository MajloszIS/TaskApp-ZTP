using System;

namespace TaskApp.Items;

public abstract class ItemBase : IItem
{
    public Guid Id { get; protected set; }
    public required string Title { get; set; }
    public DateTime CreatedAt { get; protected set; }

    public ItemBase()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public abstract IItem Clone();
}