using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;
using PizzaApi.Core.PriceCalculations;

namespace PizzaApi.Core.Extensions;

public static class OrderExtensions
{
    public static OrderDto ToDto(this Order order)
        => new(order.Id, order.CreationDate, order.State, order.Size.ToDto(), order.Toppings);

    public static List<OrderDto> ToDto(this IEnumerable<Order> orders)
        => orders.Select(x => x.ToDto()).ToList();

    public static PriceRequest ToPriceRequest(this CreateOrderDto order) =>
        new() { PizzaSizeId = order.PizzaSize.Id, Toppings = order.Toppings };
}