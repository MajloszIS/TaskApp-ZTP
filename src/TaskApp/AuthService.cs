using System;
using TaskApp.Repository;
using TaskApp.Exceptions;

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
            throw new ValidationException("Username and password cannot be empty.");
        }

        var newUser = new User(username, password);
        userRepository.AddUser(newUser);
    }
    public bool Login(string username, string password)
    {
        if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            throw new ValidationException("Username and password cannot be empty.");
        }
        var user = userRepository.GetUserByUsername(username);
        if(user.VerifyPassword(password))
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
            throw new ValidationException("No user is currently logged in.");
        }
        currentUser = null;
    }
    public User GetCurrentUser()
    {
        if(currentUser == null)
        {
            throw new ValidationException("No user is currently logged in.");
        }
        return currentUser;
    }
    public User GetUserByUsername(string username)
    {
        return userRepository.GetUserByUsername(username);
    }
}