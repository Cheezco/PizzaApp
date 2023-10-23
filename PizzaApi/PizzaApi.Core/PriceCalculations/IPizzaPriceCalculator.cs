using PizzaApi.Core.Entities;

namespace PizzaApi.Core.PriceCalculations;

public interface IPizzaPriceCalculator
{
    public PriceResponse GetPrice(PizzaSize pizzaSize, List<PriceRequestTopping> toppings);
}