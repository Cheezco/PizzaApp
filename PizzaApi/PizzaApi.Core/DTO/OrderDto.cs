using PizzaApi.Core.Enums;

namespace PizzaApi.Core.DTO;

public record OrderDto(int Id, DateTime CreationDate, OrderState State, PizzaSizeDto PizzaSize,
    List<ToppingDto> Toppings);

public record NewOrderDto(PizzaSizeDto PizzaSize, List<ToppingDto> Toppings);