using System;
using TaskApp.Items;
using TaskApp.Observer;

namespace TaskApp.Commands;

public class SaveStateCommand : ItemCommandBase
{
    public SaveStateCommand(ItemManager itemManager, IItem item) 
        : base(itemManager, item)
    {
    }

    public override void Execute()
    {
        var snapshot = item.Clone();
        var memento = new ItemMemento(snapshot);

        itemManager.SetBackup(item.Id, memento);
        
        Console.WriteLine($"State of '{item.Title}' has been saved.");
    }

    public override void Undo()
    {
        Console.WriteLine("Undoing backup is not needed.");
    }
}