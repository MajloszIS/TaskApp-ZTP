using System;

namespace TaskApp.Items;

public class Note : ItemBase
{
    public string? Content;
    public List<string>? Tags;

    public Note(string content, List<string> tags, string title)
    {
        this.Content = content;
        this.Tags = tags;
        this.Title = title;   
    }

    public override IItem Clone()
    {
        return new Note
        (
            content = this.Content,
            tags = this.Tags
        );
    }
}
