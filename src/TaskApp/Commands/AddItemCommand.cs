using System;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;

namespace TaskApp.Commands;

public class AddItemCommand : ItemCommandBase
{

    private ItemGroup? parentGroup;

    public AddItemCommand(ItemManager itemManager, IItem item, ItemGroup? parentGroup = null)
        : base(itemManager, item)
    {
        this.parentGroup = parentGroup;
    }

    public override void Execute()
    {
        if (parentGroup != null)
            parentGroup.Add(item);
        else
            itemManager.AddItem(item);
    }
    public override void Undo()
    {
        if (parentGroup != null)
            parentGroup.Remove(item);
        else
            itemManager.RemoveItem(item);
    }
}