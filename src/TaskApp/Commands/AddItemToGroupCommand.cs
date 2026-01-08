using TaskApp.Items;

namespace TaskApp.Commands;

public class AddItemToGroupCommand : ItemCommandBase
{
    private readonly ItemGroup parentGroup;

    public AddItemToGroupCommand(User user, IItem item, ItemGroup parentGroup)
        : base(null, user, item)
    {
        this.parentGroup = parentGroup;
    }

    public override void Execute()
    {
        parentGroup.Add(item);
    }

    public override void Undo()
    {
        parentGroup.Remove(item);
    }
}