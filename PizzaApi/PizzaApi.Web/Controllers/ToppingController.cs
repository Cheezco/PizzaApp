using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;
using PizzaApi.Core.Extensions;
using PizzaApi.Core.Misc;
using PizzaApi.Core.Specifications;
using PizzaApi.Infrastructure.Interfaces;

namespace PizzaApi.Web.Controllers;

[Route("api/topping-categories/{toppingCategoryId:int}/toppings")]
[ApiController]
public class ToppingController : ControllerBase
{
    private readonly IRepository<ToppingCategory> _toppingCategoryRepository;
    private readonly IRepository<Topping> _toppingRepository;

    public ToppingController(IRepository<ToppingCategory> toppingCategoryRepository,
        IRepository<Topping> toppingRepository)
    {
        _toppingCategoryRepository = toppingCategoryRepository;
        _toppingRepository = toppingRepository;
    }

    [HttpGet(Name = "GetToppings")]
    public async Task<IEnumerable<ToppingDto>> GetMany(int toppingCategoryId, int page = PaginationHelper.DefaultPage,
        int pageSize = PaginationHelper.DefaultPageSize)
    {
        var spec = new ToppingsSpec(toppingCategoryId);
        var toppings = await _toppingRepository.ListAsync(spec);

        var totalCount = await _toppingRepository.CountAsync(spec);
        var totalPages = PaginationHelper.CalculateTotalPages(pageSize, totalCount);
        var currentPage = PaginationHelper.GetCurrentPage(totalCount, page);
        var fixedPageSize = PaginationHelper.CalculatePageSize(pageSize);

        var previousPageLink = PaginationHelper.HasPreviousPage(totalPages, currentPage)
            ? CreateResourceUri(currentPage, fixedPageSize,
                ResourceUriType.PreviousPage)
            : null;

        var nextPageLink = PaginationHelper.HasNextPage(totalPages, currentPage)
            ? CreateResourceUri(currentPage, fixedPageSize, ResourceUriType.NextPage)
            : null;

        var paginationMetadata = new
        {
            totalCount,
            pageSize = fixedPageSize,
            currentPage,
            totalPages,
            previousPageLink,
            nextPageLink
        };

        Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

        return toppings.ToDto();
    }

    [HttpGet("{toppingId:int}")]
    public async Task<IActionResult> Get(int toppingCategoryId, int toppingId)
    {
        var spec = new ToppingByIdSpec(toppingCategoryId, toppingId);
        var topping = await _toppingRepository.FirstOrDefaultAsync(spec);

        if (topping is null) return NotFound();

        return Ok(topping.ToDto());
    }

    private string? CreateResourceUri(int page, int pageSize, ResourceUriType type)
    {
        return type switch
        {
            ResourceUriType.PreviousPage => Url.Link("GetToppings", new
            {
                page = page - 1,
                pageSize
            }),
            ResourceUriType.NextPage => Url.Link("GetToppings", new
            {
                page = page + 1,
                pageSize
            }),
            _ => string.Empty
        };
    }
}