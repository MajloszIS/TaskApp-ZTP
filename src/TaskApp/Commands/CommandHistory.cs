using System;
using System.Collections.Generic;
using TaskApp.Repository;

namespace TaskApp.Commands;

public class CommandHistory
{
    private Stack<ICommand> undoStack = new Stack<ICommand>();
    private Stack<ICommand> redoStack = new Stack<ICommand>();

    public void Execute(ICommand cmd)
    {
        cmd.Execute();
        undoStack.Push(cmd);
        redoStack.Clear();
    }
    public void Undo()
    {
        if (undoStack.Count == 0) {
            return;
            }

            var cmd = undoStack.Pop();
            cmd.Undo();
            redoStack.Push(cmd);
    }
    public void Redo()
    {
        if(redoStack.Count == 0)
        {
            return;
        }
        var cmd = redoStack.Pop();
        cmd.Execute();
        undoStack.Push(cmd);
    }
}