using PizzaApi.Core.Enums;

namespace PizzaApi.Core.DTO;

public record OrderDto(int Id, DateTime CreationDate, OrderState State, PizzaSizeDto PizzaSize,
    List<ToppingDto> Toppings);

public record CreateOrderDto(PizzaSizeDto PizzaSize, List<ToppingDto> Toppings);

public record UpdateOrderDto(int Id, OrderState State);