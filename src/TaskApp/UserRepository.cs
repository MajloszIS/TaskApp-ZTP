using System;
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

    }
    public User GetByUsername(string username)
    {

    }
    public void Add(User user)
    {

    }
}