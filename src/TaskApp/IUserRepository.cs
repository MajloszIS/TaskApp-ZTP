using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;

public interface IUserRepository
{
    User GetById(Guid id);
    User GetByUsername(string username);
    void Add(User user);
}