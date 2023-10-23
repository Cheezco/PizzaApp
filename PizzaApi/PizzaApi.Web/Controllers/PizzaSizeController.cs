using System.Security.Policy;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;
using PizzaApi.Core.Extensions;
using PizzaApi.Core.Misc;
using PizzaApi.Core.Specifications;
using PizzaApi.Infrastructure.Interfaces;

namespace PizzaApi.Web.Controllers;

[Route("api/pizza-sizes")]
[ApiController]
public class PizzaSizeController : ControllerBase
{
    private readonly IRepository<PizzaSize> _pizzaSizeRepository;

    public PizzaSizeController(IRepository<PizzaSize> pizzaSizeRepository)
    {
        _pizzaSizeRepository = pizzaSizeRepository;
    }

    [HttpGet(Name = "GetPizzaSizes")]
    public async Task<IEnumerable<PizzaSizeDto>> GetMany(int page = PaginationHelper.DefaultPage,
        int pageSize = PaginationHelper.DefaultPageSize)
    {
        var pizzaSizeSpec = new PizzaSizesSpec();

        var pizzaSizes = await _pizzaSizeRepository.ListAsync(pizzaSizeSpec);

        var totalCount = await _pizzaSizeRepository.CountAsync(pizzaSizeSpec);
        var totalPages = PaginationHelper.CalculateTotalPages(pageSize, totalCount);
        var currentPage = PaginationHelper.GetCurrentPage(totalCount, page);
        var fixedPageSize = PaginationHelper.CalculatePageSize(pageSize);

        var previousPageLink = PaginationHelper.HasPreviousPage(totalPages, currentPage)
            ? CreateResourceUri(currentPage, fixedPageSize, ResourceUriType.PreviousPage)
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

        return pizzaSizes.ToDto();
    }

    [HttpGet("{pizzaSizeId}")]
    public async Task<IActionResult> Get(int pizzaSizeId)
    {
        var spec = new PizzaSizeByIdSpec(pizzaSizeId);
        var pizzaSize = await _pizzaSizeRepository.FirstOrDefaultAsync(spec);

        if (pizzaSize is null) return NotFound();

        return Ok(pizzaSize.ToDto());
    }

    private string? CreateResourceUri(int page, int pageSize, ResourceUriType type)
    {
        return type switch
        {
            ResourceUriType.PreviousPage => Url.Link("GetGroups", new
            {
                page = page - 1,
                pageSize
            }),
            ResourceUriType.NextPage => Url.Link("GetGroups", new
            {
                page = page + 1,
                pageSize
            }),
            _ => string.Empty
        };
    }
}