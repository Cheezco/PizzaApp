using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;
using PizzaApi.Core.Extensions;
using PizzaApi.Core.Misc;
using PizzaApi.Core.Specifications;
using PizzaApi.Infrastructure.Interfaces;

namespace PizzaApi.Web.Controllers;

[Microsoft.AspNetCore.Components.Route("api/topping-categories")]
[ApiController]
public class ToppingCategoryController : ControllerBase
{
    private readonly IRepository<ToppingCategory> _toppingCategoryRepository;

    public ToppingCategoryController(IRepository<ToppingCategory> toppingCategoryRepository)
    {
        _toppingCategoryRepository = toppingCategoryRepository;
    }

    [HttpGet(Name = "GetToppingCategories")]
    public async Task<IEnumerable<ToppingCategoryDto>> GetMany(int page = PaginationHelper.DefaultPage,
        int pageSize = PaginationHelper.DefaultPageSize)
    {
        var toppingCategorySpec = new ToppingCategoriesSpec();
        var toppingCategories = await _toppingCategoryRepository.ListAsync(toppingCategorySpec);

        var totalCount = await _toppingCategoryRepository.CountAsync(toppingCategorySpec);
        var totalPages = PaginationHelper.CalculateTotalPages(pageSize, totalCount);
        var currentPage = PaginationHelper.GetCurrentPage(totalPages, page);
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

        return toppingCategories.ToDto();
    }

    [HttpGet("{toppingCategoryId}")]
    public async Task<IActionResult> Get(int toppingCategoryId)
    {
        var spec = new ToppingCategoryByIdSpec(toppingCategoryId);
        var toppingCategory = await _toppingCategoryRepository.FirstOrDefaultAsync(spec);

        if (toppingCategory is null) return NotFound();

        return Ok(toppingCategory.ToDto());
    }

    private string? CreateResourceUri(int page, int pageSize, ResourceUriType type)
    {
        return type switch
        {
            ResourceUriType.PreviousPage => Url.Link("GetToppingCategories", new
            {
                page = page - 1,
                pageSize
            }),
            ResourceUriType.NextPage => Url.Link("GetToppingCategories", new
            {
                page = page + 1,
                pageSize
            }),
            _ => string.Empty
        };
    }
}