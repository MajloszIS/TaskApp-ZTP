using System;
using TaskApp.Items;
using TaskApp.Observer;

namespace TaskApp.Commands;

public class RestoreStateCommand : ItemCommandBase
{
    private IItem? previousState;

    public RestoreStateCommand(ItemManager itemManager, IItem item) 
        : base(itemManager, item)
    {
    }

    public override void Execute()
    {
        var memento = itemManager.GetBackup(item.Id);

        if (memento != null)
        {
            previousState = item.Clone();

            item.Restore(memento.StateSnapshot);
            itemManager.UpdateItem(item);
            
            Console.WriteLine($"Item '{item.Title}' restored from backup.");
        }
        else
        {
            throw new Exception("No backup found for this item!");
        }
    }

    public override void Undo()
    {
        if (previousState != null)
        {
            item.Restore(previousState);
            itemManager.UpdateItem(item);
            Console.WriteLine($"Undo Restore: Reverted '{item.Title}' to state before backup restore.");
        }
    }
}