using System;
using TaskApp.Items;
using TaskApp.Observer;

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
        if (item is Note note)
        {
            note.Content = newContent;
        }
        
        itemManager.UpdateItem(item);
    }

    public override void Undo()
    {
        if (backup == null) return;

        var oldState = backup.StateSnapshot;

        item.Title = oldState.Title;

        if (item is Note liveNote && oldState is Note snapshotNote)
        {
            liveNote.Content = snapshotNote.Content;
            
            if (snapshotNote.Tags != null)
                liveNote.Tags = new List<string>(snapshotNote.Tags);
            else
                liveNote.Tags = new List<string>();
        }
        else if (item is Tasky liveTask && oldState is Tasky snapshotTask)
        {
            liveTask.DueDate = snapshotTask.DueDate;
            liveTask.Priority = snapshotTask.Priority;
            liveTask.IsCompleted = snapshotTask.IsCompleted;
        }

        itemManager.UpdateItem(item);
    }
}