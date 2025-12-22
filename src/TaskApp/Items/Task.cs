using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;

namespace TaskApp.Items;

public class Task : ItemBase
{
    public bool IsCompleted;
    public DateTime DueDate;
    public int Priority;

    public override IItem Clone() { return null; }
}