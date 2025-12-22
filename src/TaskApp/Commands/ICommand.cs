using System;
using System.Collections.Generic;
using TaskApp.Items;
using TaskApp.Commands;

namespace TaskApp.Commands;

public interface ICommand
{
    void Execute();
    void Undo();
}