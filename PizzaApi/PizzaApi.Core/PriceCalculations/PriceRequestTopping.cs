namespace PizzaApi.Core.PriceCalculations;

public record PriceRequestTopping(int Id, int CategoryId, int Count, decimal Price);