using System;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;

namespace TaskApp.Commands;

public class EditItemCommand : ItemCommandBase
{
    private string newTitle;
    private string newContent;

    public EditItemCommand(ItemManager itemManager, IItem item, string title, string content)
        : base(itemManager, item)
    {
        this.newTitle = title; 
        this.newContent = content;
    }

    public override void Execute()
    {
        CreateBackup();
        item.Title = newTitle;
        if(item is Note note)
        {
            note.Content = newContent;
        }

        itemManager.UpdateItem(item);
    }
    public override void Undo()
    {
        item.Title = backup.title;
        if(item is Note note)
        {
            note.Content = backup.datasnapshot;
        }

        itemManager.UpdateItem(item);
    }
}