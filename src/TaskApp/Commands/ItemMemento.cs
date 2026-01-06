using System;
using TaskApp.Repository;

namespace TaskApp.Commands;

public class ItemMemento
{
    public Guid itemid;
    public string title;
    public string datasnapshot;

    public ItemMemento(Guid itemid, string title, string datasnapshot)
    {
        this.itemid = itemid;
        this.title = title;
        this.datasnapshot = datasnapshot;
    }
}