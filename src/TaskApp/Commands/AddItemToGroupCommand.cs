using TaskApp.Items;

namespace TaskApp.Commands;
public class AddItemToGroupCommand : ICommand
{
    private readonly ItemGroup group;
    private readonly IItem item;

    public AddItemToGroupCommand(ItemGroup group, IItem item)
    {
        this.group = group;
        this.item = item;
    }

    public void Execute()
    {
        group.Add(item);
    }

    public void Undo()
    {
        group.Remove(item);
    }
}
