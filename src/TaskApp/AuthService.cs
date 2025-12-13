using System;

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

    }
    public bool Login(string username, string password)
    {

    }
    public void Logout()
    {

    }
    public User GetCurrentUser()
    {

    }
    private string FakeHashMethod(string password)
    {

    }
}