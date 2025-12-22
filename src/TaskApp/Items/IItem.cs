using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;

namespace TaskApp.Items;

public interface IItem
{
    Guid Id { get; }
    string Title { get; set; }
    DateTime CreatedAt { get; }
    IItem Clone();
}