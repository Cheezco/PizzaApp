using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Extensions;

public static class OrderExtensions
{
    public static OrderDto ToDto(this Order order)
        => new(order.Id, order.CreationDate, order.State, order.Size.ToDto(), order.Toppings.ToDto());
}