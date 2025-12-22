using System;

namespace TaskApp.Items;

public class PinnedItemDecorator : ItemDecorator
{
    private DateTime PinnedAt;

    public PinnedItemDecorator(IItem innerItem) : base(innerItem) { }

    public override IItem Clone() { return null; }
}
