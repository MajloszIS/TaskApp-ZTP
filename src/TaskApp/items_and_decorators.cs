using System;
using System.Collections.Generic;

namespace NotesApp.Items
{
    public interface IItem
    {
        Guid Id { get; }
        string Title { get; set; }
        DateTime CreatedAt { get; }
        IItem Clone();
    }

    public abstract class ItemBase : IItem
    {
        public Guid Id { get; protected set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; protected set; }

        public abstract IItem Clone();
    }

    public class Note : ItemBase
    {
        public string Content;
        public List<string> Tags;

        public override IItem Clone() { return null; }
    }

    public class Task : ItemBase
    {
        public bool IsCompleted;
        public DateTime DueDate;
        public int Priority;

        public override IItem Clone() { return null; }
    }

    public class ItemGroup : ItemBase
    {
        private List<IItem> Children;

        public void Add(IItem item) { }
        public void Remove(IItem item) { }
        public List<IItem> GetChildren() { return null; }

        public override IItem Clone() { return null; }
    }

    public abstract class ItemDecorator : IItem
    {
        protected IItem innerItem;

        public Guid Id => innerItem.Id;
        public string Title { get => innerItem.Title; set => innerItem.Title = value; }
        public DateTime CreatedAt => innerItem.CreatedAt;

        protected ItemDecorator(IItem innerItem)
        {
            this.innerItem = innerItem;
        }

        public abstract IItem Clone();
    }

    public class PinnedItemDecorator : ItemDecorator
    {
        private DateTime PinnedAt;

        public PinnedItemDecorator(IItem innerItem) : base(innerItem) { }

        public override IItem Clone() { return null; }
    }
}
