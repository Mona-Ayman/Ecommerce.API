

namespace Domain.Exceptions
{
    public sealed class UnAuthorizedException(string message = "Invalid email or password") : Exception(message)
    {
    }
}
