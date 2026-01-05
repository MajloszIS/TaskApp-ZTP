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
    protected User user;
    protected ItemMemento? backup;

    public ItemCommandBase(ItemManager itemManager, User user, IItem item)
    {
        this.item = item;
        this.itemManager = itemManager;
        this.user = user;
    }

    protected void CreateBackup()
    {
        string content = "";
        if (item is Note note)
        {
            content = note.Content ?? string.Empty;
        }
        backup = new ItemMemento(item.Id, item.Title, content);
    }
    public abstract void Execute();
    public abstract void Undo();
}