using System;

namespace TaskApp.Items;

public class Note : ItemBase
{
    public string? Content;
    public List<string>? Tags;

    public Note(string title, string content)
                :base(title)
    {
        this.Content = content;
        this.Tags = new List<string>();
    }
    public override IItem Clone()
    {
        return new Note(this.Title, this.Content ?? "");
    }
}
