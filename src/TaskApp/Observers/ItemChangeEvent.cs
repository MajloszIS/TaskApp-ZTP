using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;

namespace TaskApp.Observer;

public class ItemChangeEvent
{
    public string ChangeType { get; }
    public IItem Item { get; }
    public User User { get; }

    public ItemChangeEvent(string changeType, IItem item, User user)
    {

    }
}