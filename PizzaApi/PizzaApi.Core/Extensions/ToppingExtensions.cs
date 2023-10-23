using System.Collections;
using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Extensions;

public static class ToppingExtensions
{
    public static ToppingDto ToDto(this Topping topping) => new(topping.Id, topping.ToppingCategoryId, topping.Name,
        topping.Limit, topping.Price);

    public static List<SelectedToppingDto> ToSelectedDto(this IEnumerable<Topping> toppings)
    {
        var result = new List<SelectedToppingDto>();
        foreach (var topping in toppings)
        {
            var item = result.FirstOrDefault(x => x.Id == topping.Id);

            if (item is null)
            {
                result.Add(new SelectedToppingDto
                {
                    Id = topping.Id,
                    CategoryId = topping.ToppingCategoryId,
                    Count = 1
                });
            }
            else
            {
                item.Count += 1;
            }
        }

        return result;
    }

    public static List<ToppingDto> ToDto(this IEnumerable<Topping> toppings)
        => toppings.Select(x => x.ToDto()).ToList();
}