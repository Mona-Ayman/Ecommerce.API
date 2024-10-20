
using AutoMapper;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Shared;
using Shared.OrderModels;

namespace Services.MappingProfiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, ShippingAddressDTO>();
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId,
                options => options.MapFrom(s => s.Product.ProductId))
                 .ForMember(d => d.ProductName,
                options => options.MapFrom(s => s.Product.ProductName))
                  .ForMember(d => d.PictureUrl,
                options => options.MapFrom(s => s.Product.PictureUrl));

            CreateMap<Order, OrderResultDTO>()
                .ForMember(d => d.PaymentStatus,
                 options => options.MapFrom(s => s.ToString()))
                 .ForMember(d => d.DeliveryMethods,
                 options => options.MapFrom(s => s.DeliveryMethods.ShortName))
                 .ForMember(d => d.Total,
                  options => options.MapFrom(s => s.Subtotal + s.DeliveryMethods.Price
                 ));

            CreateMap<DeliveryMethod, DeliveryMethodDTO>();

            CreateMap<AddressDTO, Address>().ReverseMap();
        }
    }
}
