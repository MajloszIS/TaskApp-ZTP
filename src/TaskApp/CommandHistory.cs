using System;
using System.Collections.Generic;
using NotesApp.Items;

namespace NotesApp.Commands
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class ItemMemento
    {
        public Guid ItemId;
        public string Title;
        public string DataSnapshot;
    }

    public abstract class ItemCommandBase : ICommand
    {
        protected IItem item;
        protected ItemMemento backup;

        public abstract void Execute();
        public abstract void Undo();
    }

    public class AddItemCommand : ItemCommandBase
    {
        private ItemGroup parentGroup;

        public override void Execute() { }
        public override void Undo() { }
    }

    public class EditItemCommand : ItemCommandBase
    {
        private string newTitle;
        private string newData;

        public override void Execute() { }
        public override void Undo() { }
    }

    public class DeleteItemCommand : ItemCommandBase
    {
        private ItemGroup parentGroup;

        public override void Execute() { }
        public override void Undo() { }
    }

    public class CloneItemCommand : ItemCommandBase
    {
        private ItemGroup targetGroup;

        public override void Execute() { }
        public override void Undo() { }
    }

    public class ShareItemCommand : ItemCommandBase
    {
        private User targetUser;

        public override void Execute() { }
        public override void Undo() { }
    }

    public class CommandHistory
    {
        private Stack<ICommand> undoStack;
        private Stack<ICommand> redoStack;

        public void Execute(ICommand cmd) { }
        public void Undo() { }
        public void Redo() { }
    }
}
