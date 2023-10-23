using PizzaApi.Core.Entities;

namespace PizzaApi.Core.PriceCalculations;

public class PizzaPriceCalculator : IPizzaPriceCalculator
{
    public decimal GetPrice(PizzaSize pizzaSize, List<Topping> toppings)
    {
        var price = pizzaSize.Price;

        toppings.ForEach(x => price += x.Price);

        if (toppings.Count > 3)
        {
            price *= 0.9m;
        }

        return price;
    }
}