namespace Services;

public class UserFriendlyException : Exception
{
    public UserFriendlyException(string message) : base(message)
    {
        
    }
}