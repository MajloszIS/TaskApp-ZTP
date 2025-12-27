using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Commands;

public class CloneItemCommand : ItemCommandBase
{
    private ItemGroup targetGroup;

    public CloneItemCommand(ItemManager itemManager, User user, IItem item, ItemGroup? targetGroup)
        : base(itemManager, user, item)
    {
        this.targetGroup = targetGroup;
    }

    public override void Execute() { }
    public override void Undo() { }
}