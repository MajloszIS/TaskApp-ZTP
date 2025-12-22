using System;

namespace TaskApp.Items;

public class Note : ItemBase
{
    public string? Content;
    public List<string>? Tags;

    public override IItem Clone()
    {
        return new Note
        {
            Content = this.Content,
            Tags = this.Tags,
            Title = this.Title
        };
    }
}
