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
        var newGroup = new ItemGroup(this.Title);
        foreach (var item in Children) 
        { 
            var childClone = item.Clone(); 
            if (childClone != null) 
            {
                newGroup.Add(childClone); 
            }
        }
        newGroup.Title = this.Title; 
        return newGroup;
    }


}