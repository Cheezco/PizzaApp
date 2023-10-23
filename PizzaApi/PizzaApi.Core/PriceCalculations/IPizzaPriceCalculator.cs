using PizzaApi.Core.Entities;

namespace PizzaApi.Core.PriceCalculations;

public interface IPizzaPriceCalculator
{
    public decimal GetPrice(PizzaSize pizzaSize, List<Topping> toppings);
}