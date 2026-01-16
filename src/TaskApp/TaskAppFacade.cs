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
    if (string.IsNullOrWhiteSpace(title))
        throw new Exception("Folder title cannot be empty");

    var user = authService.GetCurrentUser();
    if (user == null)
        throw new Exception("No user logged in");

    var exists = itemManager
        .GetAllItemsForUser(user)
        .OfType<ItemGroup>()
        .Any(f => f.Title == title);

    if (exists)
        throw new Exception("Folder with this name already exists");

    var folder = new ItemGroup(title);
    var command = new AddItemCommand(itemManager, folder);
    GetHistoryForCurrentUser().Execute(command);
}

public void AddFolderToFolder(string parentFolderName, string childFolderName)
{
    var user = authService.GetCurrentUser();
    if (user == null)
        throw new Exception("No user logged in");

    var allFolders = itemManager
        .GetAllItemsForUser(user)
        .OfType<ItemGroup>()
        .ToList();

    var parentFolder = allFolders.FirstOrDefault(f => f.Title == parentFolderName);
    if (parentFolder == null)
        throw new Exception("Parent folder not found");

    if (parentFolder.Children.OfType<ItemGroup>().Any(f => f.Title == childFolderName))
        throw new Exception("Folder already exists in this folder");

    if (parentFolderName == childFolderName)
        throw new Exception("Folder cannot contain itself");

    var childFolder = new ItemGroup(childFolderName);
    parentFolder.Add(childFolder);

    itemManager.UpdateItem(parentFolder);
}

public List<ItemGroup> GetRootFolders()
{
    var user = authService.GetCurrentUser();
    if (user == null)
        throw new Exception("No user logged in");

    var allFolders = itemManager
        .GetAllItemsForUser(user)
        .OfType<ItemGroup>()
        .ToList();

    var childFolders = new HashSet<Guid>();
    foreach (var folder in allFolders)
    {
        foreach (var child in folder.Children.OfType<ItemGroup>())
        {
            childFolders.Add(child.Id);
        }
    }

    return allFolders.Where(f => !childFolders.Contains(f.Id)).ToList();
}

public void AddItemToFolder(Guid folderId, Guid itemId)
{
    var user = authService.GetCurrentUser();
    if (user == null)
        throw new Exception("No user logged in");

    var folder = itemManager.GetItemById(folderId) as ItemGroup;
    if (folder == null)
        throw new Exception("Folder not found");

    var item = itemManager.GetItemById(itemId);
    if (item == null)
        throw new Exception("Item not found");

    folder.Add(item);
    itemManager.UpdateItem(folder);
}
public void RemoveItemFromFolder(Guid folderId, Guid itemId)
{
    var folder = itemManager.GetItemById(folderId) as ItemGroup;
    if (folder == null)
        throw new Exception("Folder not found");

    var item = folder.Children.FirstOrDefault(i => i.Id == itemId);
    if (item == null)
        throw new Exception("Item not in folder");

    folder.Remove(item);
    itemManager.UpdateItem(folder);
}
public void ShareFolder(Guid folderId, string targetUsername)
{
    var folder = itemManager.GetItemById(folderId) as ItemGroup;
    if (folder == null)
        throw new Exception("Folder not found");

    var target = authService.GetUserByUsername(targetUsername);
    itemManager.ShareItem(target, folder);
}
public ItemGroup ViewFolder(Guid folderId)
{
    var folder = itemManager.GetItemById(folderId) as ItemGroup;
    if (folder == null)
        throw new Exception("Folder not found");

    return folder;
}
public void DeleteFolder(string folderName)
{
    var user = authService.GetCurrentUser();
    if (user == null)
    {
        throw new Exception("No user logged in");
    }

    var folders = itemManager
        .GetAllItemsForUser(user)
        .OfType<ItemGroup>()
        .ToList();

    ItemGroup? parent = null;
    ItemGroup? target = null;

    foreach (var f in folders)
    {
        if (f.Title == folderName)
        {
            target = f;
            break;
        }

        var child = f.Children
            .OfType<ItemGroup>()
            .FirstOrDefault(c => c.Title == folderName);

        if (child != null)
        {
            parent = f;
            target = child;
            break;
        }
    }

    if (target == null)
    {
        throw new Exception("Folder not found");
    }

    var command = new DeleteFolderCommand(itemManager, target, parent);
    GetHistoryForCurrentUser().Execute(command);
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
    public void AddFolder(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new Exception("Title cannot be empty");
        }
        var task = new ItemGroup(title);
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
    public void SaveItemState(string title)
{
    var item = itemManager.GetItemByTitle(title);
    if (item == null) throw new Exception("Item not found");

    new SaveStateCommand(itemManager, item).Execute();
}

public void RestoreItemState(string title)
{
    var item = itemManager.GetItemByTitle(title);
    if (item == null) throw new Exception("Item not found");


    var command = new RestoreStateCommand(itemManager, item);
    GetHistoryForCurrentUser().Execute(command);
}
}
