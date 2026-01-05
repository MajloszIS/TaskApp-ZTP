using System;
using System.Threading.Tasks;
using TaskApp.Access;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Repository;

public class TaskAppFacade
{
    private readonly AuthService authService;
    private readonly ItemManager itemManager;
    private readonly CommandHistory history;
    private readonly ItemQueryService queryService;
    public TaskAppFacade(IUserRepository userRepo, IItemRepository itemRepo)
    {
        authService = new AuthService(userRepo);
        itemManager = new ItemManager(itemRepo);
        history = new CommandHistory();
        queryService = new ItemQueryService(itemRepo);
    }
    public void Register(string username, string password)
    {   
        authService.Register(username, password);
    }
    public bool Login(string username, string password) 
    { 
        bool result = authService.Login(username, password);
        itemManager.SetCurrentUser(authService.GetCurrentUser());
        return result;
    }
    public void Logout()
    {
        itemManager.SetCurrentUser(null);
        authService.Logout();
    } 
    public void AddNote(string title, string content)
    {
        if(string.IsNullOrEmpty(title))
        {
            throw new Exception("Title cannot be empty");
        }
        var note = new Note(title, content);
        new AddItemCommand(itemManager, authService.GetCurrentUser(), note).Execute();
    }
    public void AddTask(string title, DateTime dueDate, int priority)
    {
        if(string.IsNullOrEmpty(title))
        {
            throw new Exception("Title cannot be empty");
        }
        var task = new Tasky(title, dueDate, priority);
        new AddItemCommand(itemManager, authService.GetCurrentUser(), task).Execute();
    }
    public void EditItem(Guid id, string newTitle, string newContent)
    {

    }
    public void CloneItem(string title)
    {
        var user = authService.GetCurrentUser();
        if (user == null)
        {
            throw new Exception("No user is logged in");
        }
        var originalItem = itemManager.GetItemByTitle(title);
        if (originalItem == null)
        {
            throw new Exception("Item not found");
        }
        new CloneItemCommand(itemManager, user, originalItem, null).Execute();
    }

    public void PinItem(string title)
    {
        var user = authService.GetCurrentUser();
        if (user == null) throw new Exception("No user is logged in");
        var item = itemManager.GetItemByTitle(title);
        if (item == null) throw new Exception("Item not found");
        if (item is PinnedItemDecorator) return;
        itemManager.RemoveItem(item);
        var pinned = new PinnedItemDecorator(item);
        itemManager.AddItem(pinned);
    }

    public void UnpinItem(string title)
    {
        var user = authService.GetCurrentUser();
        if (user == null) throw new Exception("No user is logged in");
        var item = itemManager.GetItemByTitle(title);
        if (item == null) throw new Exception("Item not found");
        if (item is PinnedItemDecorator pid)
        {
            itemManager.RemoveItem(pid);
            var inner = pid.GetInnerItem();
            itemManager.AddItem(inner);
        }
    }
    public void DeleteItem(string title)
    {
        new DeleteItemCommand(itemManager, authService.GetCurrentUser(), itemManager.GetItemByTitle(title)).Execute();
    }
    public void ShareItem(string title, string targetUsername)
    {
        var target = authService.GetUserByUsername(targetUsername);
        var owner = authService.GetCurrentUser();
        new ShareItemCommand(itemManager, owner, itemManager.GetItemByTitle(title), target).Execute();
    }
    public List<IItem> GetAllItems()
    {
        var user = authService.GetCurrentUser();
        if(user == null)
        {
            throw new Exception("No user is logged in");
        }
        return itemManager.GetAllItemsForUser(user);
    }
    public List<IItem> FilterItems(string criteria)
    {
        var list = new List<IItem>();
        return list;
    }
    public List<IItem> SearchItems(string text)
    {
        var list = new List<IItem>();
        return list;
    }
    public void Undo()
    {

    }
    public void Redo()
    {

    }
    public void AttachObserver(IItemObserver observer)
    {
        itemManager.Attach(observer);
    }
    public void DetachObserver(IItemObserver observer)
    {
        itemManager.Detach(observer);
    }
}
