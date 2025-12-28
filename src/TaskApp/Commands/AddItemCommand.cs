using System;
using System.Collections.Generic;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;

namespace TaskApp.Commands;

public class AddItemCommand : ItemCommandBase
{

    private ItemGroup? parentGroup;

    public AddItemCommand(ItemManager itemManager, User user, IItem item, ItemGroup? parentGroup = null)
        : base(itemManager, user, item)
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