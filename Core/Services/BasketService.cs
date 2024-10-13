
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Shared;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string id)
       => await basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDTO?> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            return basket is null ? throw new BasketNotFoundException(id) : mapper.Map<BasketDTO>(basket);
        }

        public async Task<BasketDTO?> UpdateBasketAsync(BasketDTO basket)
        {
            var value = mapper.Map<CustomerBasket>(basket);
            var result = await basketRepository.UpdateBasketAsync(value);
            return result is null ? throw new Exception("Can't update basket nowk") : mapper.Map<BasketDTO>(result);
        }
    }
}
