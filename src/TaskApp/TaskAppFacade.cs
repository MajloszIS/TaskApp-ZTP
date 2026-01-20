using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp.Access;
using TaskApp.Commands;
using TaskApp.Exceptions;
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
    public void Login(string username, string password) 
    { 
        authService.Login(username, password);
        itemManager.SetCurrentUser(authService.GetCurrentUser());
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
                throw new ValidationException("Title cannot be empty.");
            }
        var note = new Note(title, content);
        var command = new AddItemCommand(itemManager, note);
        GetHistoryForCurrentUser().Execute(command);
    }
    public void CreateFolder(string title)
    {
        itemManager.CreateFolder(title);
    }

    public void AddFolderToFolder(string parentFolderName, string childFolderName)
    {
        itemManager.AddFolderToFolder(parentFolderName, childFolderName);
    }

    public void DeleteFolder(string name)
    {
        itemManager.RemoveFolder(name);
    }
    public void AddItemToFolder(Guid folderId, Guid itemId)
    {
        itemManager.AddItemToFolder(folderId, itemId);
    }

    public void RemoveItemFromFolder(Guid folderId, Guid itemId)
    {
        itemManager.RemoveItemFromFolder(folderId, itemId);
    }

    public void ShareFolder(Guid folderId, string targetUsername)
    {
        var target = authService.GetUserByUsername(targetUsername)
            ?? throw new Exception("User not found");
        itemManager.ShareFolder(folderId, target);
    }
    public ItemGroup ViewFolder(Guid folderId)
    {
        return itemManager.ViewFolder(folderId);
    }

    public List<ItemGroup> GetRootFolders()
    {
        return itemManager.GetRootFolders();
    }

    public void AddTask(string title, DateTime dueDate, int priority)
    {
        if(string.IsNullOrEmpty(title))
        {
            throw new Exception("Title cannot be empty");
        }
        var task = new Tasky(title, dueDate, priority);
        var command = new AddItemCommand(itemManager, task);
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

        var command = new EditItemCommand(itemManager, foundItem, newTitle, newContent);
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
        var command = new CloneItemCommand(itemManager, originalItem, null);
        GetHistoryForCurrentUser().Execute(command);
    }

    public void PinItem(string title)
    {
        var item = itemManager.GetItemByTitle(title);
        var command = new PinItemCommand(itemManager, item, false);
        GetHistoryForCurrentUser().Execute(command);
    }

    public void UnpinItem(string title)
    {
        var item = itemManager.GetItemByTitle(title);
        var command = new PinItemCommand(itemManager, item, true);
        GetHistoryForCurrentUser().Execute(command);
    }
    public void DeleteItem(string title)
    {
        var command = new DeleteItemCommand(itemManager, itemManager.GetItemByTitle(title));
        GetHistoryForCurrentUser().Execute(command);
    }
    public void ShareItem(string title, string targetUsername)
    {
        var target = authService.GetUserByUsername(targetUsername);
        var command = new ShareItemCommand(itemManager, itemManager.GetItemByTitle(title), target);
        GetHistoryForCurrentUser().Execute(command);
    }
    public List<IItem> GetAllItems()
    {
        var user = authService.GetCurrentUser();
        if(user == null)
        {
            throw new AccessDeniedException();
        }
        return itemManager.GetAllItemsForUser(user);
    }
    public List<IItem> FilterItems(string criteria)
    {
        var user = authService.GetCurrentUser();
        return queryService.Filter(user, criteria);
    }
    public List<IItem> SearchItems(string text)
    {
        var user = authService.GetCurrentUser();
        return queryService.Search(user, text);
    }
    public List<IItem> SortItems(List<IItem> items, string mode)
    {
        return queryService.Sort(items, mode);
    }
    public void PrintAllItems()
    {
        var user = authService.GetCurrentUser();
        queryService.PrintAllItems(user);
    }
    public void PrintFilteredItems(string criteria)
    {
        var user = authService.GetCurrentUser();
        queryService.PrintFilteredItems(user, criteria);
    }
    public void PrintSortedItems(List<IItem> items, string mode)
    {
        queryService.PrintSortedItems(items, mode);
    }
    public void PrintSearchedItems(string text)
    {
        var user = authService.GetCurrentUser();
        queryService.PrintSearchedItems(user, text);
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
    public void SaveItemState(string title)
{
    var item = itemManager.GetItemByTitle(title);
    if (item == null) throw new Exception("Item not found");

}

public void RestoreItemState(string title)
{
    var item = itemManager.GetItemByTitle(title);
    if (item == null) throw new Exception("Item not found");


}
}
