using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Extensions;

public static class ToppingExtensions
{
    public static ToppingDto ToDto(this Topping topping) => new(topping.Id,  topping.ToppingCategoryId, topping.Name, topping.Limit, topping.Price);

    public static List<ToppingDto> ToDto(this IEnumerable<Topping> toppings)
        => toppings.Select(x => x.ToDto()).ToList();
}