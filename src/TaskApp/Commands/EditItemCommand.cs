using System;
using System.Collections.Generic;
using TaskApp.Items;
using TaskApp.Commands;

namespace TaskApp.Commands;

public class EditItemCommand : ItemCommandBase
{
    private string newTitle;
    private string newData;

    public override void Execute() { }
    public override void Undo() { }
}