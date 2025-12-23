using System;
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
        if(id == Guid.Empty)
        {
            throw new ArgumentException("Invalid ID");
        }

        foreach (var user in _users)
        {
            if (user.Id == id)
            {
                return user;
            }
        }

        throw new Exception("User not found");
    }
    public User GetByUsername(string username)
    {
        if(string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Invalid username");
        }

        foreach (var user in _users)
        {
            if (user.Username == username)
            {
                return user;
            }
        }

        throw new Exception("User not found");
    }
    public void Add(User user)
    {
        if(user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null");
        }

        if (_users.Any(u => u.Id == user.Id))
        {
            throw new Exception("User with the same ID already exists");
        }

        _users.Add(user);
    }
}