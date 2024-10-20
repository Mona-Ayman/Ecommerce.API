
namespace Domain.Exceptions
{
    public class UserNotFoundException(string email) : NotFoundException($"No user with email{email} was found")
    {
    }
}
