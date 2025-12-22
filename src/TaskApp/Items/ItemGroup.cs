using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;

namespace TaskApp.Items;

public class ItemGroup : ItemBase
{
    private List<IItem> Children;

    public void Add(IItem item) { }
    public void Remove(IItem item) { }
    public List<IItem> GetChildren() { return null; }

    public override IItem Clone() { return null; }
}