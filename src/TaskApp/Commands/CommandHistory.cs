using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Commands;

public class CommandHistory
{
    private Stack<ICommand> undoStack;
    private Stack<ICommand> redoStack;

    public void Execute(ICommand cmd) { }
    public void Undo() { }
    public void Redo() { }
}