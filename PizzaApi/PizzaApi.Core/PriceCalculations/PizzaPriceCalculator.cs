using PizzaApi.Core.Entities;

namespace PizzaApi.Core.PriceCalculations;

public class PizzaPriceCalculator : IPizzaPriceCalculator
{
    public PriceResponse GetPrice(PizzaSize pizzaSize, List<PriceRequestTopping> toppings)
    {
        var response = new PriceResponse
        {
            SizePrice = pizzaSize.Price,
            TotalPrice = pizzaSize.Price,
            Toppings = new List<PriceResponseTopping>()
        };

        toppings.ForEach(x =>
        {
            response.TotalPrice += x.Price * x.Count;
            response.Toppings.Add(new PriceResponseTopping
            {
                CategoryId = x.CategoryId,
                Id = x.Id,
                Count = x.Count,
                Price = x.Price * x.Count
            });
        });

        if (toppings.Count > 3)
        {
            response.TotalPrice *= 0.9m;
            response.DiscountApplied = true;
        }

        return response;
    }
}