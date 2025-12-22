using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;


namespace TaskApp.Commands;

public class ShareItemCommand : ItemCommandBase
{
    private User targetUser;

    public override void Execute() { }
    public override void Undo() { }
}