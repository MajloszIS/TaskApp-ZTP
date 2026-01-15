using System;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;

namespace TaskApp.Commands;

public class PinItemCommand : ItemCommandBase
{
    private readonly bool unpinMode;
    private IItem? pinnedItem;
    private IItem? innerItem;
    private bool executed = false;

    public PinItemCommand(ItemManager itemManager, IItem item, bool unpin = false)
        : base(itemManager, item)
    {
        this.unpinMode = unpin;
    }

    public override void Execute()
    {
    
        if (item == null)
        {
            throw new Exception("Item not found");
        }
        
        if (!unpinMode)
        {
            if (item is PinnedItemDecorator)
            {
                executed = false;
                return;
            }
            itemManager.RemoveItem(item);
            pinnedItem = new PinnedItemDecorator(item);
            itemManager.AddItem(pinnedItem);
            executed = true;
            innerItem = item;
        }
        else
        {
            if (item is PinnedItemDecorator pid)
            {
                itemManager.RemoveItem(pid);
                innerItem = pid.GetInnerItem();
                itemManager.AddItem(innerItem);
                pinnedItem = pid;
                executed = true;
            }
            else
            {
                executed = false;
            }
        }
    }

    public override void Undo()
    {
        if (!executed) return;

        if (!unpinMode)
        {
            if (pinnedItem != null)
            {
                itemManager.RemoveItem(pinnedItem);
                if (innerItem != null)
                    itemManager.AddItem(innerItem);
            }
        }
        else
        {
            if (innerItem != null)
            {
                itemManager.RemoveItem(innerItem);
                if (pinnedItem != null)
                    itemManager.AddItem(pinnedItem);
            }
        }
    }
}
