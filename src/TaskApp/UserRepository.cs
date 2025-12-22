using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using System.Collections.Generic;
using System.Linq;

public class UserRepository : IUserRepository
{
    private List<User> _users;
    public UserRepository()
    {
        _users = new List<User>();
    }
    public User GetById(Guid id)
    {
        var user = new User("","");
        return user;
    }
    public User GetByUsername(string username)
    {
        var user = new User("","");
        return user;
    }
    public void Add(User user)
    {

    }
}