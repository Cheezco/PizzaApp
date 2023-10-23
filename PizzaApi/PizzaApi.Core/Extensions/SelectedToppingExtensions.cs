using System.Collections;
using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Extensions;

public static class SelectedToppingExtensions
{
    public static SelectedToppingDto ToDto(this SelectedTopping selectedTopping)
        => new()
        {
            Id = selectedTopping.Id,
            CategoryId = selectedTopping.Topping.ToppingCategoryId,
            Count = selectedTopping.Count
        };

    public static List<SelectedToppingDto> ToDto(this IEnumerable<SelectedTopping> selectedToppings)
        => selectedToppings.Select(x => x.ToDto()).ToList();

    public static SelectedTopping FromDto(this SelectedToppingDto selectedToppingDto, int orderId)
        => new() { ToppingId = selectedToppingDto.Id, Count = selectedToppingDto.Count, OrderId = orderId };

    public static List<SelectedTopping> FromDto(this IEnumerable<SelectedToppingDto> selectedToppingDtos, int orderId)
        => selectedToppingDtos.Select(x => x.FromDto(orderId)).ToList();
}