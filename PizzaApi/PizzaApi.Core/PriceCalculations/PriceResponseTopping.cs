namespace PizzaApi.Core.PriceCalculations;

public class PriceResponseTopping
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
}