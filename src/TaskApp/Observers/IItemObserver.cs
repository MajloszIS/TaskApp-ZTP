using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;

namespace TaskApp.Observer;

public interface IItemObserver
{
    void Update(ItemChangeEvent evt);
}