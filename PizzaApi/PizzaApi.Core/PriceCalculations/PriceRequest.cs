using PizzaApi.Core.DTO;

namespace PizzaApi.Core.PriceCalculations;

public class PriceRequest
{
    public int PizzaSizeId { get; set; }
    public List<PriceRequestToppingDto> Toppings { get; set; }
}