using System;

namespace TaskApp.Items;

public class PinnedItemDecorator : ItemDecorator
{
    private DateTime PinnedAt;

    public PinnedItemDecorator(IItem innerItem) : base(innerItem)
    {
        this.PinnedAt = DateTime.Now;
    }

    public override IItem Clone()
    {
        var clonedInner = innerItem.Clone();
        var decorated = new PinnedItemDecorator(clonedInner);
        decorated.PinnedAt = this.PinnedAt;
        return decorated;
    }
}
