using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

namespace TaskApp.Commands;

public class ItemMemento
{
    public Guid ItemId;
    public string Title;
    public string DataSnapshot;
}