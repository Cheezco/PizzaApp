using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Extensions;

public static class ToppingCategoryExtensions
{
    public static ToppingCategoryDto ToDto(this ToppingCategory toppingCategory) =>
        new(toppingCategory.Name, toppingCategory.Toppings.ToDto());

    public static List<ToppingCategoryDto> ToDto(this IEnumerable<ToppingCategory> toppingCategories)
        => toppingCategories.Select(x => x.ToDto()).ToList();
}