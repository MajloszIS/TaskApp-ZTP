using System;
using TaskApp.Repository;

public class AuthService
{
    private readonly IUserRepository userRepository;
    private User? currentUser;
    public AuthService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public void Register(string username, string password)
    {
        if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            throw new Exception("Username and password cannot be empty");
        }

        var newUser = new User(username, password);
        userRepository.Add(newUser);
    }
    public bool Login(string username, string password)
    {
        if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            throw new Exception("Username and password cannot be empty");
        }
        var user = userRepository.GetByUsername(username);
        if(user.PasswordHash == password)
        {
            currentUser = user;
            return true;
        }
        return false;
    }
    public void Logout()
    {
        if(currentUser == null)
        {
            throw new Exception("No user is currently logged in");
        }
        currentUser = null;
    }
    public User GetCurrentUser()
    {
        if(currentUser == null)
        {
            throw new Exception("No user is currently logged in");
        }
        return currentUser;
    }
}