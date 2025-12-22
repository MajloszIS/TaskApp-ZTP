using System;
using TaskApp.Commands;
using TaskApp.Items;
using TaskApp.Observer;
using TaskApp.Access;
using TaskApp.Repository;
//niektóre trzeba usunąc, nie wszystkie są potrzebne

public class AuthService
{
    private readonly IUserRepository userRepository;
    private User currentUser;
    public AuthService(IUserRepository userRepository, User currentUser)
    {
        this.userRepository = userRepository;
        this.currentUser = currentUser;
    }
    public void Register(string username, string password)
    {
        var newUser = new User(username, password);
        userRepository.Add(newUser);
    }
    public bool Login(string username, string password)
    {
        return true;
    }
    public void Logout()
    {

    }
    public User GetCurrentUser()
    {
        var user = new User("","");
        return user;
    }
    private string FakeHashMethod(string password)
    {
        return "";
    }
}