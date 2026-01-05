using System;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;

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
        itemManager.ShareItem(targetUser, item);
    }
    public override void Undo() { }
}