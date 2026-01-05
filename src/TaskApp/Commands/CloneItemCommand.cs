using System;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;

namespace TaskApp.Commands;

public class CloneItemCommand : ItemCommandBase
{
    private ItemGroup targetGroup;
    private IItem? clonedItem;

    public CloneItemCommand(ItemManager itemManager, User user, IItem item, ItemGroup? targetGroup)
        : base(itemManager, user, item)
    {
        this.targetGroup = targetGroup;
    }

    public override void Execute() 
    {
        clonedItem = item.Clone();
        if (clonedItem == null) return;
        clonedItem.Title = $"{item.Title} (Copy)";
        clonedItem.Owners.Add(user);
        if (targetGroup != null)
            targetGroup.Add(clonedItem);
        else
            itemManager.AddItem(clonedItem);
    }
    public override void Undo() 
    {
        if (clonedItem == null) return;
        if (targetGroup != null)
            targetGroup.Remove(clonedItem);
        else
            itemManager.RemoveItem(clonedItem);
    }
}