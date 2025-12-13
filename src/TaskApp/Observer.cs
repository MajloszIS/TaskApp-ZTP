namespace TaskApp.Observer
{
    using System;
    using System.Collections.Generic;
    using TaskApp.Domain;
    using TaskApp.Repository;
    using TaskApp.Users;

    public class ItemChangeEvent
    {
        public string ChangeType { get; }
        public IItem Item { get; }
        public User User { get; }

        public ItemChangeEvent(string changeType, IItem item, User user)
        {

        }
    }

    public interface IItemObserver
    {
        void Update(ItemChangeEvent evt);
    }

    public interface IItemObservable
    {
        void Attach(IItemObserver observer);
        void Detach(IItemObserver observer);
        void Notify(ItemChangeEvent evt);
    }

    public class ItemManager : IItemObservable
    {
        private readonly List<IItemObserver> observers = new();
        private readonly IItemRepository repo;

        public ItemManager(IItemRepository repo)
        {

        }

        public void AddItem(User user, IItem item)
        {

        }

        public void RemoveItem(User user, IItem item)
        {

        }

        public void UpdateItem(User user, IItem item)
        {

        }

        public List<IItem> GetAllItems(User user)
        {
            
        }

        public void Attach(IItemObserver observer)
        {

        }

        public void Detach(IItemObserver observer)
        {

        }

        public void Notify(ItemChangeEvent evt)
        {

        }
    }

    public class ItemsListView : IItemObserver
    {
        public void Update(ItemChangeEvent evt)
        {

        }
    }

    public class NotificationService : IItemObserver
    {
        public void Update(ItemChangeEvent evt)
        {

        }
    }
}