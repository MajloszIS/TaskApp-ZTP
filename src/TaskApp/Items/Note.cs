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
        var clone = new Note(this.Title, this.Content ?? "");
        if (this.Tags != null)
        {
            clone.Tags = new List<string>(this.Tags);
        }
        return clone;
    }
}
