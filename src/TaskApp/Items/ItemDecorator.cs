using System;
using System.Collections.Generic;

namespace TaskApp.Items;

public abstract class ItemDecorator : IItem
{
    protected IItem innerItem;
    List<User> IItem.Owners
    {
        get => innerItem.Owners;
        set => innerItem.Owners = value;
    }

    public Guid Id => innerItem.Id;
    public string Title { get => innerItem.Title; set => innerItem.Title = value; }
    public DateTime CreatedAt => innerItem.CreatedAt;

    protected ItemDecorator(IItem innerItem)
    {
        this.innerItem = innerItem;
    }

    public virtual IItem Clone()
    {
        return innerItem.Clone();
    }

    public IItem GetInnerItem()
    {
        return innerItem;
    }
}