using System;
using TaskApp.Items;

namespace TaskApp.Commands;

public class ItemMemento
{
    public readonly IItem StateSnapshot;

    public ItemMemento(IItem stateSnapshot)
    {
        this.StateSnapshot = stateSnapshot;
    }
}