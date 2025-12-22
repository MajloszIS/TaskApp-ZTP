using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;

namespace TaskApp.Items;

public abstract class ItemBase : IItem
{
    public Guid Id { get; protected set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; protected set; }

    public abstract IItem Clone();
}