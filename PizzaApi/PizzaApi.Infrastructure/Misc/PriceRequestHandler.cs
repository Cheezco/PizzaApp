using PizzaApi.Core.Entities;
using PizzaApi.Core.PriceCalculations;
using PizzaApi.Core.Specifications;
using PizzaApi.Infrastructure.Interfaces;

namespace PizzaApi.Infrastructure.Misc;

public class PriceRequestHandler
{
    private readonly IRepository<PizzaSize> _pizzaSizeRepository;
    private readonly IRepository<Topping> _toppingRepository;
    private readonly IPizzaPriceCalculator _pizzaPriceCalculator;

    public PriceRequestHandler(IRepository<PizzaSize> pizzaSizeRepository, IRepository<Topping> toppingRepository,
        IPizzaPriceCalculator pizzaPriceCalculator)
    {
        _pizzaSizeRepository = pizzaSizeRepository;
        _toppingRepository = toppingRepository;
        _pizzaPriceCalculator = pizzaPriceCalculator;
    }

    public async Task<PriceResponse?> GetPriceAsync(PriceRequest priceRequest)
    {
        var pizzaSizeSpec = new PizzaSizeByIdSpec(priceRequest.PizzaSizeId);
        var pizzaSize = await _pizzaSizeRepository.FirstOrDefaultAsync(pizzaSizeSpec);

        if (pizzaSize is null) return null;

        var toppings = new List<PriceRequestTopping>();

        var uniqueToppings = priceRequest.Toppings
            .DistinctBy(x => x.Id)
            .ToList();

        foreach (var toppingDto in uniqueToppings)
        {
            var spec = new ToppingByIdSpec(toppingDto.CategoryId, toppingDto.Id);
            var topping = await _toppingRepository.FirstOrDefaultAsync(spec);

            if (topping is null || toppingDto.Count > topping.Limit) return null;

            toppings.Add(new PriceRequestTopping(toppingDto.Id, toppingDto.CategoryId, toppingDto.Count,
                topping.Price));
        }

        return _pizzaPriceCalculator.GetPrice(pizzaSize, toppings);
    }
}