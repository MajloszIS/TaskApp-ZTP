using System;

namespace TaskApp.Items;

public class Note : ItemBase
{
    public string? Content;
    public List<string> Tags;

    public Note(string title, string content)
                :base(title)
    {
        this.Content = content;
        this.Tags = new List<string>();
    }
    public void AddTag(string tag)
    {
        Tags.Add(tag);
    }
    public override IItem Clone()
    {
        var cloneNote = new Note(this.Title, this.Content ?? "");
        var clonedOwners = new List<User>();
        foreach(User user in this.Owners)
        {
            clonedOwners.Add(user);
        }
        cloneNote.Owners = clonedOwners;
        if (this.Tags != null)
        {
            cloneNote.Tags = new List<string>(this.Tags);
        }
        return cloneNote;
    }
    public override void Restore(IItem state)
    {
        base.Restore(state); 
        if (state is Note other)
        {
            this.Content = other.Content;

            if (other.Tags != null)
            {
                this.Tags = new List<string>(other.Tags);
            }
            else
            {
                this.Tags = new List<string>();
            }
        }
    }
}
