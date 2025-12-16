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

    }
    private string FakeHashMethod(string password)
    {

    }
}