using TaskApp.Items;

namespace TaskApp.Commands;

public class AddItemToGroupCommand : ItemCommandBase
{
    private readonly ItemGroup parentGroup;

    public AddItemToGroupCommand( IItem item, ItemGroup parentGroup)
        : base(null, item)
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