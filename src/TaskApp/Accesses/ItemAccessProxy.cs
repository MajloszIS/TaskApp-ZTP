using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Access;

public class ItemAccessProxy : IItemAccess
{
    private readonly IItemAccess innerService;
    private readonly User currentUser;
    public ItemAccessProxy(IItemAccess innerService)
    {
        this.innerService = innerService;
    }
    public IItem GetItem(User user, Guid id)
    {
        var item = new Note();
        return item;
    }
    public List<IItem> GetItemsForUser(User user)
    {
        var list = new List<IItem>();
        return list;
    }
    public void SaveItem(User user, IItem item)
    {
        
    }
    public void ShareItem(User owner, User target, IItem item)
    {
        
    }
}
