using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Extensions;

public static class PizzaSizeExtensions
{
    public static PizzaSizeDto ToDto(this PizzaSize pizzaSize) =>
        new(pizzaSize.Id, pizzaSize.Name, pizzaSize.Order, pizzaSize.Price);

    public static List<PizzaSizeDto> ToDto(this IEnumerable<PizzaSize> pizzaSizes)
        => pizzaSizes.Select(x => x.ToDto()).ToList();
}