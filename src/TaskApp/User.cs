using System;

public class User
{
    public Guid Id { get; }
    public string Username { get; }
    private string Password { get; set; }
    public User(string username, string password)
    {
        Id = Guid.NewGuid();
        Username = username;
        Password = password;
    }
    public bool VerifyPassword(string password)
    {
        return this.Password == password;
    }
}