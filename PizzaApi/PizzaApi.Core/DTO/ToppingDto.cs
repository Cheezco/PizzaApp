namespace PizzaApi.Core.DTO;

public record ToppingDto(int Id, int CategoryId, string Name, int Limit, decimal Price);

public record PriceRequestToppingDto(int Id, int CategoryId, int Count);