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
        if(authService.GetCurrentUser() == null)
        {
            throw new Exception("No user is logged in");
        }
        if(string.IsNullOrEmpty(title))
        {
            throw new Exception("Title cannot be empty");
        }
        var note = new Note(title, content);
        new AddItemCommand(itemManager, authService.GetCurrentUser(), note).Execute();
    }
    public void AddTask(string title, DateTime dueDate, int priority)
    {
        if(authService.GetCurrentUser() == null)
        {
            throw new Exception("No user is logged in");
        }
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
    public void CloneItem(Guid id)
    {

    }
    public void DeleteItemById(Guid itemId)
    {
        if(authService.GetCurrentUser() == null)
        {
            throw new Exception("No user is logged in");
        }
        
        var item = itemManager.GetItemById(itemId);

        if(item == null)
        {
            throw new Exception("Item not found");
        }
        
        new DeleteItemCommand(itemManager, authService.GetCurrentUser(), item).Execute();
    }
    public void DeleteItemByTitle(string title)
    {
        
    }
    public void ShareItem(Guid itemId, string targetUsername)
    {
        var owner = authService.GetCurrentUser();

        if (owner == null)
            throw new Exception("No user is logged in");

        var target = authService.GetByUsrn(targetUsername);

        if (target == null)
            throw new Exception("Target user not found");

        new ShareItemCommand(itemManager, owner, itemManager.GetItemById(itemId), target).Execute();
    }
    public void ShareItemByTitle(string title, string targetUsername)
    {
        var owner = authService.GetCurrentUser();

        var item = itemManager.GetItemByTitle(title);

        if (owner == null)
            throw new Exception("No user is logged in");

        var target = authService.GetByUsrn(targetUsername);

        if (target == null)
            throw new Exception("Target user not found");

        new ShareItemCommand(itemManager, owner, itemManager.GetItemById(item.Id), target).Execute();
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
}
