using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne


namespace TaskApp.Repository;

public interface IUserRepository
{
    User GetById(Guid id);
    User GetByUsername(string username);
    void Add(User user);
}