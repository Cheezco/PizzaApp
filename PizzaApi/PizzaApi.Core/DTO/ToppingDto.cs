namespace PizzaApi.Core.DTO;

public record ToppingDto(int Id, int CategoryId, string Name, int Limit, decimal Price);

public record SelectedToppingDto(int Id, int CategoryId, int Count);