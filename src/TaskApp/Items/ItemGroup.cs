using System;
using System.Collections.Generic;

namespace TaskApp.Items;

public class ItemGroup : ItemBase
{
    private List<IItem> Children { get; }

    public ItemGroup(List<IItem> items)
        : base(title: "Group")
    {
        Children = items;
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
    var clonedChildren = new List<IItem>();

    foreach (var item in Children)
    {
        clonedChildren.Add(item.Clone());
    }

    var newGroup = new ItemGroup(clonedChildren)
    {
        Title = this.Title
    };

    return newGroup;
}

}