using System;
using TaskApp.Items;

namespace TaskApp.Commands;

public class ItemMemento
{
    public IItem StateSnapshot { get; }

    public ItemMemento(IItem stateSnapshot)
    {
        this.StateSnapshot = stateSnapshot;
    }
}