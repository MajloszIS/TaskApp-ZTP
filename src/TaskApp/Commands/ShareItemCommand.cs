using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Commands;

public class ShareItemCommand : ItemCommandBase
{
    private User? targetUser;

    public ShareItemCommand(ItemManager itemManager, IItemAccess itemAccess, User user, IItem item, User targetUser)
        :base(itemManager, itemAccess, user, item)
    {
        this.targetUser = targetUser;
    }

    public override void Execute() { }
    public override void Undo() { }
}