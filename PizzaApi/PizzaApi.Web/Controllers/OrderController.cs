using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;
using PizzaApi.Core.Extensions;
using PizzaApi.Core.Misc;
using PizzaApi.Core.Specifications;
using PizzaApi.Infrastructure.Interfaces;

namespace PizzaApi.Web.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IRepository<Order> _orderRepository;

    public OrderController(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet(Name = "GetOrders")]
    public async Task<IEnumerable<OrderDto>> GetMany(int page = PaginationHelper.DefaultPage,
        int pageSize = PaginationHelper.DefaultPageSize, bool includeDraft = false, bool draftOnly = false)
    {
        var orderSpec = new OrdersSpec(page, pageSize, includeDraft, draftOnly);
        var orders = await _orderRepository.ListAsync(orderSpec);

        var totalCount = await _orderRepository.CountAsync(orderSpec);
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

        return orders.ToDto();
    }

    [HttpGet("{orderId:int}")]
    public async Task<IActionResult> Get(int orderId)
    {
        var spec = new OrderByIdSpec(orderId);
        var order = await _orderRepository.FirstOrDefaultAsync(spec);

        if (order is null) return NotFound();

        return Ok(order.ToDto());
    }

    [HttpPut("{orderId:int}")]
    public async Task<ActionResult<OrderDto>> Update(int orderId, UpdateOrderDto updateOrderDto)
    {
        var spec = new OrderByIdSpec(orderId);
        var order = await _orderRepository.FirstOrDefaultAsync(spec);

        if (order is null) return NotFound();

        order.State = updateOrderDto.State;

        await _orderRepository.UpdateAsync(order);

        return Ok(order.ToDto());
    }

    [HttpDelete("{orderId:int}")]
    public async Task<ActionResult> Remove(int orderId)
    {
        var spec = new OrderByIdSpec(orderId);
        var order = await _orderRepository.FirstOrDefaultAsync(spec);

        if (order is null) return NotFound();

        await _orderRepository.DeleteAsync(order);

        return NoContent();
    }

    private string? CreateResourceUri(int page, int pageSize, ResourceUriType type)
    {
        return type switch
        {
            ResourceUriType.PreviousPage => Url.Link("GetOrders", new
            {
                page = page - 1,
                pageSize
            }),
            ResourceUriType.NextPage => Url.Link("GetOrders", new
            {
                page = page + 1,
                pageSize
            }),
            _ => string.Empty
        };
    }
}