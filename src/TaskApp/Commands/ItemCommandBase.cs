using System;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;


namespace TaskApp.Commands;

public abstract class ItemCommandBase : ICommand
{
    protected IItem item;
    protected ItemManager itemManager;
    protected ItemMemento? backup;

    public ItemCommandBase(ItemManager itemManager, IItem item)
    {
        this.item = item;
        this.itemManager = itemManager;
    }

    protected void CreateBackup()
    {
        var snapshot = item.Clone();
        
        backup = new ItemMemento(snapshot);
    }    public abstract void Execute();
    public abstract void Undo();
}