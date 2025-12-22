using System;

namespace TaskApp.Items;

public class ItemGroup : ItemBase
{
    private List<IItem> Children { get; }

    public ItemGroup(List<IItem> items)
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

    public override IItem Clone(){ return null; }
    /*
    public override IItem Clone()
    { 
        return new ItemGroup
        {
            Title = this.Title
        }; 
    }
    */
}