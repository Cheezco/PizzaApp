using PizzaApi.Core.Entities;

namespace PizzaApi.Core.PizzaPriceCalculator;

public interface IPizzaPriceCalculator
{
    public decimal GetPrice(PizzaSize pizzaSize, List<Topping> toppings);
}