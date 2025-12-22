using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;

namespace TaskApp.Items;

public class Note : ItemBase
{
    public string Content;
    public List<string> Tags;

    public override IItem Clone() { return null; }
}