using Microsoft.AspNetCore.Mvc;
using PizzaApi.Core.Entities;
using PizzaApi.Core.PriceCalculations;
using PizzaApi.Core.Specifications;
using PizzaApi.Infrastructure.Interfaces;
using PizzaApi.Infrastructure.Misc;

namespace PizzaApi.Web.Controllers;

[Route("api/price")]
[ApiController]
public class PriceController : ControllerBase
{
    private readonly PriceRequestHandler _priceRequestHandler;

    public PriceController(PriceRequestHandler priceRequestHandler)
    {
        _priceRequestHandler = priceRequestHandler;
    }

    [HttpPost(Name = "CalculatePrice")]
    public async Task<ActionResult<PriceResponse>> CalculatePrice(PriceRequest priceRequest)
    {
        var priceResponse = await _priceRequestHandler.GetPriceAsync(priceRequest);

        if (priceResponse is null) return BadRequest();

        return priceResponse;
    }
}