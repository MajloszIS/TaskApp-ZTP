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
        itemManager = new ItemManager(itemRepo);
        history = new CommandHistory();
        itemAccess = new ItemAccessProxy(new RealItemAccessService(itemRepo, userRepo), authService.GetCurrentUser());
        queryService = new ItemQueryService(itemRepo);

    }
    public void Register(string username, string password)
    {   
        if(authService.GetCurrentUser() != null)
        {
            throw new Exception("A user is already logged in");
        }
        authService.Register(username, password);
    }
    public bool Login(string username, string password) 
    { 
        if(authService.GetCurrentUser() != null)
        {
            throw new Exception("A user is already logged in");
        }
        return authService.Login(username, password);
    }
    public void AddNote(string title, string content)
    {
        
    }
    public void AddTask(string title, DateTime dueDate ,string content)
    {

    }
    public void EditItem(Guid id, string newTitle, string newContent)
    {

    }
    public void CloneItem(Guid id)
    {

    }
    public void DeleteItem(Guid id)
    {

    }
    public void ShareItem(Guid id, string targetUsername)
    {

    }
    public void GetAllItems(List<IItem> items)
    {

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
