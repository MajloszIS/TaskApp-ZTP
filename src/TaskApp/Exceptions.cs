using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Exceptions;

// Baza dla wszystkich naszych błędów
public abstract class TaskAppException : Exception
{
    protected TaskAppException(string message) : base(message) { }
}

// Błędy autoryzacji
public class InvalidCredentialsException : TaskAppException
{
    public InvalidCredentialsException() : base("Invalid username or password.") { }
}

public class UserAlreadyExistsException : TaskAppException
{
    public UserAlreadyExistsException(string username) : base($"User '{username}' already exists.") { }
}

// Błędy związane z przedmiotami
public class ItemNotFoundException : TaskAppException
{
    public ItemNotFoundException() : base("The requested item was not found.") { }
}

public class AccessDeniedException : TaskAppException
{
    public AccessDeniedException() : base("You do not have permission to access this item.") { }
}

public class ValidationException : TaskAppException
{
    public ValidationException(string message) : base(message) { }
}
public class InvalidSortModeException : TaskAppException
{
    public InvalidSortModeException(string mode) : base($"Sort mode '{mode}' is not supported.") { }
}

public class InvalidFilterSyntaxException : TaskAppException
{
    public InvalidFilterSyntaxException(string token) : base($"Invalid filter syntax or value: '{token}'.") { }
}
