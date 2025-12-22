using System;
using System.Collections.Generic;
using TaskApp.Items;
using TaskApp.Commands;

namespace TaskApp.Commands;

public class AddItemCommand : ItemCommandBase
{
    private ItemGroup parentGroup;

    public override void Execute() { }
    public override void Undo() { }
}