
namespace Domain.Exceptions
{
    public class BasketNotFoundException(string id)
        : NotFoundException($"Basket with id {id} not found")
    {

    }
}
