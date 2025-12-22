using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Commands;

public abstract class ItemCommandBase : ICommand
{
    protected IItem item;
    protected ItemMemento backup;

    public abstract void Execute();
    public abstract void Undo();
}