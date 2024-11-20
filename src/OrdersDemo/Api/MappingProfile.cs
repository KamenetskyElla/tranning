using AutoMapper;
using OrdersDemo.Api.Models;
using OrdersDemo.Domain;

namespace OrdersDemo.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderItem, OrderItemDto>();

        CreateMap<Customer, OrderListDto>()
            .ForMember(x => x.Type, opt => opt.MapFrom(x => x.Type.ToString()));
        CreateMap<Address, OrderListDto>();
        CreateMap<Order, OrderListDto>()
            .IncludeMembers(x => x.Customer, x => x.Address);

        CreateMap<Customer, OrderDto>()
            .ForMember(x => x.Type, opt => opt.MapFrom(x => x.Type.ToString()));
        CreateMap<Address, OrderDto>();
        CreateMap<Order, OrderDto>()
            .IncludeMembers(x => x.Customer, x => x.Address);
    }
}
