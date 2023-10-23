namespace PizzaApi.Core.PriceCalculations;

public class PriceResponse
{
    public decimal TotalPrice { get; set; }
    public decimal SizePrice { get; set; }
    public bool DiscountApplied { get; set; }
    public List<PriceResponseTopping> Toppings { get; set; }
}