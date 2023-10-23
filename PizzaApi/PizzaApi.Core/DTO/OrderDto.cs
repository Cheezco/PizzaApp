using PizzaApi.Core.Enums;

namespace PizzaApi.Core.DTO;

public record OrderDto(int Id, DateTime CreationDate, OrderState State, PizzaSizeDto PizzaSize,
    List<SelectedToppingDto> Toppings);

public record CreateOrderDto(PizzaSizeDto PizzaSize, List<SelectedToppingDto> Toppings, bool IsDraft = false);

public record UpdateOrderDto(int Id, OrderState State);