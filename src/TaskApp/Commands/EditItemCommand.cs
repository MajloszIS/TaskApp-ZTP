using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Commands;

public class EditItemCommand : ItemCommandBase
{
    private string newTitle;
    private string newData;

    public EditItemCommand(ItemManager itemManager, IItemAccess itemAccess, User user, IItem item, string title, string data)
        : base(itemManager, itemAccess, user, item)
    {
        this.newTitle = item.Title;
        this.newData = data;
    }

    public override void Execute() { }
    public override void Undo() { }
}