using System;
using System.Collections.Generic;
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
    private readonly Dictionary<Guid, CommandHistory> userHistories;
    private readonly ItemQueryService queryService;
    public TaskAppFacade(IUserRepository userRepo, IItemRepository itemRepo)
    {
        authService = new AuthService(userRepo);
        itemManager = new ItemManager(itemRepo);
        userHistories = new Dictionary<Guid, CommandHistory>();
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
        var command = new AddItemCommand(itemManager, authService.GetCurrentUser(), note);
        GetHistoryForCurrentUser().Execute(command);
    }

    public void CreateFolder(string title)
{
    if (string.IsNullOrEmpty(title))
        throw new Exception("Folder title cannot be empty");

    var folder = new ItemGroup(new List<IItem>());
    folder.Title = title;

    new AddItemCommand(itemManager, authService.GetCurrentUser(), folder)
        .Execute();
}

public void AddItemToFolder(string folderTitle, string itemTitle)
{
    var user = authService.GetCurrentUser();
    if (user == null)
        throw new Exception("No user logged in");

    var folder = GetAllItems().Find(i => i is ItemGroup g && g.Title == folderTitle) as ItemGroup;
    if (folder == null)
        throw new Exception($"Folder '{folderTitle}' not found");

    var item = GetAllItems().Find(i => i.Title == itemTitle);
    if (item == null)
        throw new Exception($"Item '{itemTitle}' not found");

    folder.Add(item);
    itemManager.UpdateItem(folder);
}



    public void AddTask(string title, DateTime dueDate, int priority)
    {
        if(string.IsNullOrEmpty(title))
        {
            throw new Exception("Title cannot be empty");
        }
        var task = new Tasky(title, dueDate, priority);
        var command = new AddItemCommand(itemManager, authService.GetCurrentUser(), task);
        GetHistoryForCurrentUser().Execute(command);
    }
    public void EditItem(Guid id, string newTitle, string newContent)
    {
        var user = authService.GetCurrentUser();
        var allItems = itemManager.GetAllItemsForUser(user);
        IItem foundItem = null;
        foreach (var item in allItems)
        {
            if (item.Id == id)
            {
                foundItem = item;
                break;
            }
        }
        if (foundItem == null){
            throw new Exception("Item not found");
            }

        var command = new EditItemCommand(itemManager, user, foundItem, newTitle, newContent);
        GetHistoryForCurrentUser().Execute(command);
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
        var command = new CloneItemCommand(itemManager, user, originalItem, null);
        GetHistoryForCurrentUser().Execute(command);
    }

    public void PinItem(string title)
    {
        var item = itemManager.GetItemByTitle(title);
        new PinItemCommand(itemManager, authService.GetCurrentUser(), item, false).Execute();
    }

    public void UnpinItem(string title)
    {
        var item = itemManager.GetItemByTitle(title);
        new PinItemCommand(itemManager, authService.GetCurrentUser(), item, true).Execute();
    }
    public void DeleteItem(string title)
    {
        new DeleteItemCommand(itemManager, authService.GetCurrentUser(), itemManager.GetItemByTitle(title)).Execute();
    }
    public void ShareItem(string title, string targetUsername)
    {
        var target = authService.GetUserByUsername(targetUsername);
        var owner = authService.GetCurrentUser();
        var command = new ShareItemCommand(itemManager, owner, itemManager.GetItemByTitle(title), target);
        GetHistoryForCurrentUser().Execute(command);
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
        GetHistoryForCurrentUser().Undo();
    }
    public void Redo()
    {
        GetHistoryForCurrentUser().Redo();
    }
    public void AttachObserver(IItemObserver observer)
    {
        itemManager.Attach(observer);
    }
    public void DetachObserver(IItemObserver observer)
    {
        itemManager.Detach(observer);
    }

    private CommandHistory GetHistoryForCurrentUser()
    {
        var user = authService.GetCurrentUser();
        
        if (!userHistories.ContainsKey(user.Id))
        {
            userHistories[user.Id] = new CommandHistory();
        }

        return userHistories[user.Id];
    }
}
