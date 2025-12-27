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

    public ShareItemCommand(ItemManager itemManager, User user, IItem item, User targetUser)
        :base(itemManager,  user, item)
    {
        this.targetUser = targetUser;
    }

    public override void Execute() 
    {
        itemManager.ShareItem(user.Id, targetUser.Id, item.Id);
    }
    public override void Undo() { }
}