using System;
using System.Collections.Generic;

namespace NotesApp
{
    // =============================
    // DOMAIN: KOMPOZYT + PROTOTYPE
    // =============================

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
        public string Content { get; set; }
        public List<string> Tags { get; set; }

        public override IItem Clone() { throw new NotImplementedException(); }
    }

    public class Task : ItemBase
    {
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }

        public override IItem Clone() { throw new NotImplementedException(); }
    }

    public class ItemGroup : ItemBase
    {
        private List<IItem> Children;

        public void Add(IItem item) { }
        public void Remove(IItem item) { }
        public List<IItem> GetChildren() { return null; }
        public override IItem Clone() { throw new NotImplementedException(); }
    }

    // =============================
    // DECORATOR
    // =============================

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

        public override IItem Clone() { throw new NotImplementedException(); }
    }

    // =============================
    // COMMAND + MEMENTO
    // =============================

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

    // =============================
    // USERS + AUTORYZACJA
    // =============================

    public class User
    {
        public Guid Id;
        public string Username;
        public string PasswordHash;
    }

    public interface IUserRepository
    {
        User GetById(Guid id);
        User GetByUsername(string username);
        void Add(User user);
    }

    public class UserRepository : IUserRepository
    {
        private List<User> users;
        public User GetById(Guid id) { return null; }
        public User GetByUsername(string username) { return null; }
        public void Add(User user) { }
    }

    public class AuthService
    {
        private IUserRepository userRepository;
        private User currentUser;

        public void Register(string username, string password) { }
        public bool Login(string username, string password) { return false; }
        public void Logout() { }
        public User GetCurrentUser() { return null; }
    }

    // =============================
    // REPOSITORY + QUERY
    // =============================

    public interface IItemRepository
    {
        IItem GetById(Guid id);
        List<IItem> GetAllForUser(User user);
        void Add(User user, IItem item);
        void Update(User user, IItem item);
        void Delete(User user, IItem item);
    }

    public class ItemRepository : IItemRepository
    {
        private Dictionary<Guid, List<IItem>> itemsByUser;
        public IItem GetById(Guid id) { return null; }
        public List<IItem> GetAllForUser(User user) { return null; }
        public void Add(User user, IItem item) { }
        public void Update(User user, IItem item) { }
        public void Delete(User user, IItem item) { }
    }

    public class ItemQueryService
    {
        private IItemRepository repo;
        public List<IItem> Filter(User user, string criteria) { return null; }
        public List<IItem> Sort(List<IItem> items, string mode) { return null; }
        public List<IItem> Search(User user, string text) { return null; }
    }

    // =============================
    // PROXY
    // =============================

    public interface IItemAccess
    {
        IItem GetItem(User user, Guid id);
        List<IItem> GetItemsForUser(User user);
        void SaveItem(User user, IItem item);
        void ShareItem(User owner, User target, IItem item);
    }

    public class RealItemAccessService : IItemAccess
    {
        private IItemRepository itemRepo;
        private IUserRepository userRepo;
        public IItem GetItem(User user, Guid id) { return null; }
        public List<IItem> GetItemsForUser(User user) { return null; }
        public void SaveItem(User user, IItem item) { }
        public void ShareItem(User owner, User target, IItem item) { }
    }

    public class ItemAccessProxy : IItemAccess
    {
        private IItemAccess innerService;
        private User currentUser;
        public IItem GetItem(User user, Guid id) { return null; }
        public List<IItem> GetItemsForUser(User user) { return null; }
        public void SaveItem(User user, IItem item) { }
        public void ShareItem(User owner, User target, IItem item) { }
    }

    // =============================
    // OBSERVER
    // =============================

    public class ItemChangeEvent
    {
        public string ChangeType;
        public IItem Item;
        public User User;
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
        private List<IItemObserver> observers;
        private IItemRepository repo;

        public void AddItem(User user, IItem item) { }
        public void RemoveItem(User user, IItem item) { }
        public void UpdateItem(User user, IItem item) { }
        public List<IItem> GetAllItems(User user) { return null; }
        public void Attach(IItemObserver observer) { }
        public void Detach(IItemObserver observer) { }
        public void Notify(ItemChangeEvent evt) { }
    }

    public class ItemsListView : IItemObserver
    {
        public void Update(ItemChangeEvent evt) { }
    }

    public class NotificationService : IItemObserver
    {
        public void Update(ItemChangeEvent evt) { }
    }

    // =============================
    // APPLICATION FACADE
    // =============================

    public class NotesAppFacade
    {
        private AuthService authService;
        private ItemManager itemManager;
        private CommandHistory history;
        private IItemAccess itemAccess;
        private ItemQueryService queryService;

        public void Register(string username, string password) { }
        public bool Login(string username, string password) { return false; }
        public void Logout() { }
        public void AddNote(string title, string content) { }
        public void AddTask(string title, DateTime dueDate) { }
        public void EditItem(Guid id, string newTitle, string newData) { }
        public void CloneItem(Guid id) { }
        public void DeleteItem(Guid id) { }
        public void ShareItem(Guid id, string targetUsername) { }
        public List<IItem> GetAllItems() { return null; }
        public List<IItem> FilterItems(string criteria) { return null; }
        public List<IItem> SearchItems(string text) { return null; }
        public void Undo() { }
        public void Redo() { }
    }
}
