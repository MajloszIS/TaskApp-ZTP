using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;

public class NotesAppFacade
{
    private readonly AuthService authService;
    private readonly ItemManager itemManager;
    private readonly CommandHistory history;
    private readonly IItemAccess itemAccess;
    private readonly ItemQueryService queryService;
    public NotesAppFacade(IUserRepository userRepo, IItemRepository itemRepo)
    {
        authService = new AuthService(userRepo, new User("",""));
        queryService = new ItemQueryService(itemRepo);
        history = new CommandHistory();
        itemManager = new ItemManager(new ItemRepository());
        itemAccess = null;
    }
    public void Register(string username, string password)
    {
        authService.Register(username, password);
    }
    public bool Login(string username, string password) 
    { 
        return true;
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
