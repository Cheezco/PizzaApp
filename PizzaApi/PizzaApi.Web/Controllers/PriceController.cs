using Microsoft.AspNetCore.Mvc;
using PizzaApi.Core.Entities;
using PizzaApi.Core.PriceCalculations;
using PizzaApi.Core.Specifications;
using PizzaApi.Infrastructure.Interfaces;

namespace PizzaApi.Web.Controllers;

[Route("api/price")]
[ApiController]
public class PriceController : ControllerBase
{
    private readonly IRepository<PizzaSize> _pizzaSizeRepository;
    private readonly IRepository<Topping> _toppingRepository;
    private readonly IPizzaPriceCalculator _pizzaPriceCalculator;

    public PriceController(IRepository<PizzaSize> pizzaSizeRepository, IRepository<Topping> toppingRepository,
        IPizzaPriceCalculator pizzaPriceCalculator)
    {
        _pizzaSizeRepository = pizzaSizeRepository;
        _toppingRepository = toppingRepository;
        _pizzaPriceCalculator = pizzaPriceCalculator;
    }

    [HttpPost(Name = "CalculatePrice")]
    public async Task<ActionResult<decimal>> CalculatePrice(PriceRequest priceRequest)
    {
        var pizzaSizeSpec = new PizzaSizeByIdSpec(priceRequest.PizzaSizeId);
        var pizzaSize = await _pizzaSizeRepository.FirstOrDefaultAsync(pizzaSizeSpec);

        if (pizzaSize is null) return BadRequest();

        var toppings = new List<PriceRequestTopping>();

        var uniqueToppings = priceRequest.Toppings
            .DistinctBy(x => x.Id)
            .ToList();

        foreach (var toppingDto in uniqueToppings)
        {
            var spec = new ToppingByIdSpec(toppingDto.CategoryId, toppingDto.Id);
            var topping = await _toppingRepository.FirstOrDefaultAsync(spec);

            if (topping is null || toppingDto.Count > topping.Limit) return BadRequest();

            toppings.Add(new PriceRequestTopping(toppingDto.Count, topping.Price));
        }

        return Ok(_pizzaPriceCalculator.GetPrice(pizzaSize, toppings));
    }
}