using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;

public class TaskAppFacade
{
    private readonly AuthService authService;
    private readonly ItemManager itemManager;
    private readonly CommandHistory history;
    private readonly IItemAccess itemAccess;
    private readonly ItemQueryService queryService;
    public TaskAppFacade(IUserRepository userRepo, IItemRepository itemRepo)
    {
        authService = new AuthService(userRepo);
        itemManager = new ItemManager(new ItemRepository());
        history = new CommandHistory();
        itemAccess = new ItemAccessProxy(new RealItemAccessService(itemRepo, userRepo));
        queryService = new ItemQueryService(itemRepo);

    }
    public void Register(string username, string password)
    {   
        authService.Register(username, password);
    }
    public bool Login(string username, string password) 
    { 
        return authService.Login(username, password);
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
        new AddItemCommand(itemManager, itemAccess, authService.GetCurrentUser()!, note).Execute();
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
        new AddItemCommand(itemManager, itemAccess, authService.GetCurrentUser(), task).Execute();
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
        
        var item = itemAccess.GetItem(authService.GetCurrentUser().Id, itemId);

        if(item == null)
        {
            throw new Exception("Item not found");
        }
        
        new DeleteItemCommand(itemManager, itemAccess, authService.GetCurrentUser(), item).Execute();
    }
    public void DeleteItemByTitle(string title)
    {
        
    }
    public void ShareItem(Guid id, string targetUsername)
    {

    }
    public void GetAllItems(List<IItem> items)
    {
        if(authService.GetCurrentUser() == null)
        {
            throw new Exception("No user is logged in");
        }
        var userId = authService.GetCurrentUser().Id;
        var userItems = itemAccess.GetItemsForUser(userId);
        items.AddRange(userItems);
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
