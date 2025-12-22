using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;


namespace TaskApp.Commands;

public class ItemMemento
{
    public Guid ItemId;
    public string Title;
    public string DataSnapshot;
}