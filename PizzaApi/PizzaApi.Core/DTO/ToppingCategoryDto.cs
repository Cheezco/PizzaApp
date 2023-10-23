namespace PizzaApi.Core.DTO;

public record ToppingCategoryDto(string Name, List<ToppingDto> Toppings);