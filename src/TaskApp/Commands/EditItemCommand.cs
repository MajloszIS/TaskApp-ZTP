using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Commands;

public class EditItemCommand : ItemCommandBase
{
    private string newTitle;
    private string newData;

    public override void Execute() { }
    public override void Undo() { }
}