using AutoMapper;
using Domain.Entities;
using Shared;

namespace Services.MappingProfiles
{
    internal class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
