using System;
public class ItemQueryService
{
    private readonly IItemRepository repo;
    public ItemQueryService(IItemRepository repo)
    {
        this.repo = repo;
    }
    public List<IItem> Filter(User user, string criteria)
    {
        
    }
    public List<IItem> Sort(List<IItem> items, string mode)
    {
        
    }
    public List<IItem> Search(User user, string text)
    {

    }
}
