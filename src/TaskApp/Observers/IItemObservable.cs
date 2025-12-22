using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;

namespace TaskApp.Observer;

public interface IItemObservable
{
    void Attach(IItemObserver observer);
    void Detach(IItemObserver observer);
    void Notify(ItemChangeEvent evt);
}