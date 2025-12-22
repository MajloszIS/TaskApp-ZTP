using System;
using System.Collections.Generic;
using TaskApp.Items;
using TaskApp.Commands;

namespace TaskApp.Commands;

public class CommandHistory
{
    private Stack<ICommand> undoStack;
    private Stack<ICommand> redoStack;

    public void Execute(ICommand cmd) { }
    public void Undo() { }
    public void Redo() { }
}