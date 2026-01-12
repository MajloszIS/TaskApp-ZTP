using System;

namespace TaskApp.Items;

public class Tasky : ItemBase
{
    public bool IsCompleted { get; set; }
    public DateTime DueDate;
    public int Priority;

    public Tasky(string title, DateTime dueDate, int priority)
            :base(title)
    {
        this.IsCompleted = false;
        this.DueDate = dueDate;
        this.Priority = priority;
    }
    public override IItem Clone()
    {
        return new Tasky(this.Title, this.DueDate, this.Priority)
        {
            IsCompleted = this.IsCompleted
        };
    }

    public override void Restore(IItem state)
    {
        base.Restore(state); 
        if (state is Tasky other)
        {
            this.Priority = other.Priority;
            this.DueDate = other.DueDate;
            this.IsCompleted = other.IsCompleted;
        }
    }
}
