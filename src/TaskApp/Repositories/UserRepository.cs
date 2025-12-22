using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

using System.Collections.Generic;
using System.Linq;

namespace TaskApp.Repository;

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