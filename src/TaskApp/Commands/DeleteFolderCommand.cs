using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Commands;

public class DeleteFolderCommand : ICommand
{
    private readonly ItemManager manager;
    private readonly ItemGroup folder;
    private readonly ItemGroup? parent;

    public DeleteFolderCommand(
        ItemManager manager,
        ItemGroup folder,
        ItemGroup? parent)
    {
        this.manager = manager;
        this.folder = folder;
        this.parent = parent;
    }

    public void Execute()
    {
        if (parent == null)
        {
            manager.RemoveItem(folder);
        }
        else
        {
            parent.Remove(folder);
            manager.UpdateItem(parent);
        }
    }

    public void Undo()
    {
        if (parent == null)
        {
            manager.AddItem(folder);
        }
        else
        {
            parent.Add(folder);
            manager.UpdateItem(parent);
        }
    }
}
