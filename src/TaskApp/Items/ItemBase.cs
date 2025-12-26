using System;

namespace TaskApp.Items;

public abstract class ItemBase : IItem
{
    public Guid Id { get; protected set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; protected set; }

    public ItemBase(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
        CreatedAt = DateTime.Now;
    }

    public abstract IItem Clone();
}