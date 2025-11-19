using Application.Order.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Order;

public class OrderMappings : Profile
{
    public OrderMappings()
    {
        CreateMap<OrderItem, OrderItemDto>();
    }
}
