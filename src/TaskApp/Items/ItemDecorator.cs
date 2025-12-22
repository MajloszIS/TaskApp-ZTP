using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;

namespace TaskApp.Items;

public abstract class ItemDecorator : IItem
{
    protected IItem innerItem;

    public Guid Id => innerItem.Id;
    public string Title { get => innerItem.Title; set => innerItem.Title = value; }
    public DateTime CreatedAt => innerItem.CreatedAt;

    protected ItemDecorator(IItem innerItem)
    {
        this.innerItem = innerItem;
    }

    public abstract IItem Clone();
}