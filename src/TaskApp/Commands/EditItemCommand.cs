using System;
using TaskApp.Items;
using TaskApp.Observer;

namespace TaskApp.Commands;

public class EditItemCommand : ItemCommandBase
{
    private string newTitle;
    private string newContent;
    private string oldTitle;
    private string? oldContent;

    public EditItemCommand(ItemManager itemManager, IItem item, string title, string content)
        : base(itemManager, item)
    {
        this.newTitle = title;
        this.newContent = content;
    }

    public override void Execute()
    {
        oldTitle = item.Title;

        if (item is Note note)
        {
            oldContent = note.Content;
        }

        item.Title = newTitle;

        if (item is Note n)
        {
            n.Content = newContent;
        }
        itemManager.UpdateItem(item);
    }

    public override void Undo()
    {
        item.Title = oldTitle;

        if (item is Note note && oldContent != null)
        {
            note.Content = oldContent;
        }
        itemManager.UpdateItem(item);
    }
}