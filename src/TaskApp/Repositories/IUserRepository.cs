using System;

namespace TaskApp.Repository;

public interface IUserRepository
{
    User GetUserById(Guid id);
    User GetUserByUsername(string username);
    void AddUser(User user);
}