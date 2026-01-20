using System;
using System.Collections.Generic;

namespace TaskApp.Items;

public class ItemGroup : ItemBase
{
    public List<IItem> Children { get; }

    public ItemGroup(string title) :
        base(title)
    {
        Children = new List<IItem>();
    }

    public void Add(IItem item)
    {
        Children.Add(item);
    }

    public void Remove(IItem item)
    {
        Children.Remove(item);
    }

public override IItem Clone()
{
    var clone = new ItemGroup(this.Title);

    clone.Owners = new List<User>(this.Owners);

    foreach (var child in Children)
    {
        clone.Add(child);
    }

    return clone;
}
}