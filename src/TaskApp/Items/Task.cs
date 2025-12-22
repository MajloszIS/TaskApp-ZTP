using System;

namespace TaskApp.Items;

public class Task : ItemBase
{
    public bool IsCompleted;
    public DateTime DueDate;
    public int Priority;

    public override IItem Clone()
    {
        return new Task
        {
            IsCompleted = this.IsCompleted,
            DueDate = this.DueDate,
            Priority = this.Priority,
            Title = this.Title
        };
    }
}
