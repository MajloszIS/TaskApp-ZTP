using System;
using System.Collections.Generic;

namespace TaskApp.Items;

public abstract class ItemBase : IItem
{
    public Guid Id { get; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; }
    public List<User> Owners { get; set; }

    public ItemBase(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
        CreatedAt = DateTime.Now;
        Owners = new List<User>();
    }

    public List<User> GetOwners()
    {
        return Owners;
    }

    public abstract IItem Clone();
}