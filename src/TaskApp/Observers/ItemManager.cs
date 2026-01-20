using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Observer;

public class ItemManager : IItemObservable
{
    private readonly List<IItemObserver> observers = new List<IItemObserver>();
    private readonly ItemAccessProxy itemAccess;
    private User? currentUser;
    private Dictionary<Guid, ItemMemento> backups = new Dictionary<Guid, ItemMemento>();

    public ItemManager(IItemRepository itemRepo)
    {
        itemAccess = new ItemAccessProxy(new RealItemAccessService(itemRepo));
    }

    public IItem? GetItemById(Guid itemId)
    {
        return itemAccess.GetItemById(itemId);
    }
    public IItem? GetItemByTitle(string title)
    {
        return itemAccess.GetItemByTitle(title);
    }
    public List<IItem> GetAllItemsForUser(User user)
    {
        return itemAccess.GetAllItemsForUser(user);
    }
    public void AddItem(IItem item)
    {
        itemAccess.AddItem(item);
        var user = currentUser ?? new User("System", "");
        Notify(new ItemChangeEvent("DODANO", item, user));
    }
    public void UpdateItem(IItem item)
    {
        itemAccess.UpdateItem(item);

        var user = currentUser ?? new User("System", "");
        Notify(new ItemChangeEvent("ZAKTUALIZOWANO", item, user));
    }
    public void RemoveItem(IItem item)
    {
        itemAccess.DeleteItem(item);

        var user = currentUser ?? new User("System", "");
        Notify(new ItemChangeEvent("USUNIĘTO", item, user));
    }
    public void ShareItem(User targetUser, IItem item)
    {
        itemAccess.ShareItem(targetUser, item);
    }
    public void SetCurrentUser(User? user)
    {
        currentUser = user;
        itemAccess.SetCurrentUser(user);
    }
    public void Attach(IItemObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IItemObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(ItemChangeEvent evt)
    {
        foreach (var observer in observers)
        {
            observer.Update(evt);
        }
    }

    public void UnShareItem(User target, IItem item)
    {
        itemAccess.UnShareItem(target, item);
    }

    public void CreateFolder(string title)
    {
        if (currentUser == null)
            throw new Exception("No user logged in");

        var exists = GetAllItemsForUser(currentUser)
        .OfType<ItemGroup>()
        .Any(f => f.Title == title);

        if (exists)
            throw new Exception("Folder already exists");

        AddItem(new ItemGroup(title));
    }
public void AddFolderToFolder(string parentTitle, string childTitle)
{
    if (currentUser == null)
        throw new Exception("No user logged in");

    var parent = GetItemByTitle(parentTitle) as ItemGroup;
    if (parent == null)
        throw new Exception("Parent folder not found");

    if (parent.Children
        .OfType<ItemGroup>()
        .Any(f => f.Title == childTitle))
    {
        throw new Exception("Folder already exists in this folder");
    }

    if (parentTitle == childTitle)
        throw new Exception("Folder cannot contain itself");

    var child = new ItemGroup(childTitle);
    AddItem(child);
    parent.Add(child);
    UpdateItem(parent);
}
public void RemoveFolder(string folderName)
{
    if (currentUser == null)
        throw new Exception("No user logged in");

    var allFolders = GetAllItemsForUser(currentUser)
        .OfType<ItemGroup>()
        .ToList();

    ItemGroup? parent = null;
    ItemGroup? target = null;

    foreach (var folder in allFolders)
    {
        if (folder.Title == folderName)
        {
            target = folder;
            break;
        }

        var child = folder.Children
            .OfType<ItemGroup>()
            .FirstOrDefault(f => f.Title == folderName);

        if (child != null)
        {
            parent = folder;
            target = child;
            break;
        }
    }

    if (target == null)
        throw new Exception("Folder not found");
    parent?.Remove(target);
    RemoveItem(target);
    if (parent != null)
        UpdateItem(parent);
}

public List<ItemGroup> GetRootFolders()
{
    if (currentUser == null)
        throw new Exception("No user logged in");

    var allFolders = GetAllItemsForUser(currentUser)
        .OfType<ItemGroup>()
        .ToList();

    var childFolderIds = new HashSet<Guid>();

    foreach (var folder in allFolders)
    {
        foreach (var child in folder.Children.OfType<ItemGroup>())
        {
            childFolderIds.Add(child.Id);
        }
    }

    return allFolders
        .Where(f => !childFolderIds.Contains(f.Id))
        .ToList();
}
public void AddItemToFolder(Guid folderId, Guid itemId)
{
    if (currentUser == null)
        throw new Exception("No user logged in");

    var folder = GetItemById(folderId) as ItemGroup
        ?? throw new Exception("Folder not found");

    var item = GetItemById(itemId)
        ?? throw new Exception("Item not found");

    folder.Add(item);
    UpdateItem(folder);
}
public void RemoveItemFromFolder(Guid folderId, Guid itemId)
{
    if (currentUser == null)
        throw new Exception("No user logged in");

    var folder = GetItemById(folderId) as ItemGroup
        ?? throw new Exception("Folder not found");

    var item = folder.Children.FirstOrDefault(i => i.Id == itemId)
        ?? throw new Exception("Item not in folder");

    folder.Remove(item);
    UpdateItem(folder);
}
public void ShareFolder(Guid folderId, User targetUser)
{
    if (currentUser == null)
        throw new Exception("No user logged in");

    var folder = GetItemById(folderId) as ItemGroup
        ?? throw new Exception("Folder not found");

    ShareItem(targetUser, folder);
}
public ItemGroup ViewFolder(Guid folderId)
{
    if (currentUser == null)
        throw new Exception("No user logged in");

    return GetItemById(folderId) as ItemGroup
        ?? throw new Exception("Folder not found");
}

    public void SetBackup(Guid id, ItemMemento memento)
    {
        backups[id] = memento;  
        Console.WriteLine("Backup saved in storage");
    }

    public ItemMemento? GetBackup(Guid id)
    {
        if(backups.ContainsKey(id)){
            return backups[id];
        }
        return null;
    }
}
