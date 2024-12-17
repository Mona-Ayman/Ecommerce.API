
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared;

namespace Services
{
    //public sealed class ServiceManager : IServiceManager
    //{
    //    private readonly Lazy<IProductService> _productService;
    //    private readonly Lazy<IBasketService> _basketService;
    //    private readonly Lazy<IAuthenticationService> _authenticationService;
    //    private readonly Lazy<IOrderService> _orderService;
    //    private readonly Lazy<IPaymentService> _paymentService;
    //    private readonly Lazy<ICacheService> _cacheService;
    //    public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,
    //        IBasketRepository basketRepository,
    //        UserManager<User> userManager,
    //        IOptions<JwtOptions> options,
    //        IConfiguration configuration,
    //        ICacheRepository repository)
    //    {
    //        _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
    //        _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
    //        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, options, mapper));
    //        _orderService = new Lazy<IOrderService>(() => new OrderService(unitOfWork, basketRepository, mapper));
    //        _paymentService = new Lazy<IPaymentService>(() => new PaymentService(basketRepository, unitOfWork, configuration, mapper));
    //        _cacheService = new Lazy<ICacheService>(() => new CacheService(repository));
    //    }

    public sealed class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,
            IBasketRepository basketRepository,
            UserManager<User> userManager,
            IOptions<JwtOptions> options,
            IConfiguration configuration,
            ICacheRepository repository) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketService> _basketService = new(() => new BasketService(basketRepository, mapper));
        private readonly Lazy<IAuthenticationService> _authenticationService = new(() => new AuthenticationService(userManager, options, mapper));
        private readonly Lazy<IOrderService> _orderService = new(() => new OrderService(unitOfWork, basketRepository, mapper));
        private readonly Lazy<IPaymentService> _paymentService = new(() => new PaymentService(basketRepository, unitOfWork, configuration, mapper));
        private readonly Lazy<ICacheService> _cacheService = new(() => new CacheService(repository));

        public IProductService ProductService => _productService.Value;
        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IOrderService OrderService => _orderService.Value;

        public IPaymentService PaymentService => _paymentService.Value;
        public ICacheService CacheService => _cacheService.Value;
    }
}
