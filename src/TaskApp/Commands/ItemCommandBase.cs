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
    protected ItemManager itemManager;
    protected User user;
    protected ItemMemento? backup; //todo

    public ItemCommandBase(ItemManager itemManager, User user, IItem item)
    {
        this.item = item;
        this.itemManager = itemManager;
        this.user = user;
    }

    public abstract void Execute();
    public abstract void Undo();
}