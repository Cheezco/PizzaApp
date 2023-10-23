namespace PizzaApi.Core.DTO;

public record ToppingCategoryDto(int Id, string Name, List<ToppingDto> Toppings);