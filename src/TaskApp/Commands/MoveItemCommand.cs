using TaskApp.Items;

namespace TaskApp.Commands
{
    public class MoveItemCommand : ICommand
    {
        private readonly ItemGroup sourceParent;
        private readonly ItemGroup targetParent;
        private readonly IItem item;

        public MoveItemCommand(ItemGroup sourceParent, ItemGroup targetParent, IItem item)
        {
            this.sourceParent = sourceParent;
            this.targetParent = targetParent;
            this.item = item;
        }

        public void Execute()
        {
            sourceParent?.Remove(item);
            targetParent.Add(item);
        }

        public void Undo()
        {
            targetParent.Remove(item);
            sourceParent?.Add(item);
        }
    }
}
