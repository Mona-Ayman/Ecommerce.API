
namespace Domain.Exceptions
{
    public class DeliveryMethodMethodNotFoundException(int id) : NotFoundException($"No delivery method with id {id} was found!")
    {
    }
}
