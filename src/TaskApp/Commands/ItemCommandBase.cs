using System;
using System.Collections.Generic;
using TaskApp.Items;
using TaskApp.Commands;

namespace TaskApp.Commands;

public abstract class ItemCommandBase : ICommand
{
    protected IItem item;
    protected ItemMemento backup;

    public abstract void Execute();
    public abstract void Undo();
}