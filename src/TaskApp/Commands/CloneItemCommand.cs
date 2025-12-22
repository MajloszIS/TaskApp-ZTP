using System;
using System.Collections.Generic;
using TaskApp.Items;
using TaskApp.Commands;

namespace TaskApp.Commands;

public class CloneItemCommand : ItemCommandBase
{
    private ItemGroup targetGroup;

    public override void Execute() { }
    public override void Undo() { }
}