using System;
using System.Collections.Generic;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;

namespace TaskApp.Commands;

public class AddItemCommand : ItemCommandBase
{

    private ItemGroup? parentGroup;

    public AddItemCommand(ItemManager itemManager, IItemAccess itemAccess, User user, IItem item, ItemGroup? parentGroup = null)
        : base(itemManager, itemAccess, user, item)
    {
        this.parentGroup = parentGroup;
    }

    public override void Execute()
    {
        if (parentGroup != null)
            parentGroup.Add(item);
        else
            itemManager.AddItem(user, item);
    }
    public override void Undo()
    {
        if (parentGroup != null)
            parentGroup.Remove(item);
        else
            itemManager.RemoveItem(user, item);
    }
}