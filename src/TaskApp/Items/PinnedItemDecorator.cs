using System;
using System.Collections.Generic;
using TaskApp.Commands;
using TaskApp.Items;

namespace TaskApp.Items;

public class PinnedItemDecorator : ItemDecorator
{
    private DateTime PinnedAt;

    public PinnedItemDecorator(IItem innerItem) : base(innerItem) { }

    public override IItem Clone() { return null; }
}
