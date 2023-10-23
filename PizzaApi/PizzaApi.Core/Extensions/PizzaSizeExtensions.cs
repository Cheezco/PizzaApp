using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Extensions;

public static class PizzaSizeExtensions
{
    public static PizzaSizeDto ToDto(this PizzaSize pizzaSize) =>
        new(pizzaSize.Id, pizzaSize.Name, pizzaSize.Order, pizzaSize.Price);
}