
namespace Domain.Exceptions
{
    public class OrderNotFoundException(Guid id) : NotFoundException($"No order with id {id} was found!")
    {
    }
}
