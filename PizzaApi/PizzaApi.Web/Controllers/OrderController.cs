using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaApi.Core.Auth.Models;
using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;
using PizzaApi.Core.Enums;
using PizzaApi.Core.Extensions;
using PizzaApi.Core.Misc;
using PizzaApi.Core.Specifications;
using PizzaApi.Infrastructure.Interfaces;
using PizzaApi.Infrastructure.Misc;

namespace PizzaApi.Web.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<PizzaSize> _pizzaSizeRepository;
    private readonly PriceRequestHandler _priceRequestHandler;
    private readonly IAuthorizationService _authorizationService;

    public OrderController(IRepository<Order> orderRepository, IRepository<PizzaSize> pizzaSizeRepository,
        PriceRequestHandler priceRequestHandler, IAuthorizationService authorizationService)
    {
        _orderRepository = orderRepository;
        _pizzaSizeRepository = pizzaSizeRepository;
        _priceRequestHandler = priceRequestHandler;
        _authorizationService = authorizationService;
    }

    [HttpGet(Name = "GetOrders")]
    [Authorize(Roles = Roles.User)]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetMany(int page = PaginationHelper.DefaultPage,
        int pageSize = PaginationHelper.DefaultPageSize, bool includeDraft = false, bool draftOnly = false)
    {
        var orderSpec = new OrdersSpec(page, pageSize, includeDraft, draftOnly);
        var orders = await _orderRepository.ListAsync(orderSpec);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, orders, PolicyNames.ResourceOwner);

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

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

        return Ok(orders.ToDto());
    }

    [HttpGet("{orderId:int}")]
    [Authorize(Roles = Roles.User)]
    public async Task<IActionResult> Get(int orderId)
    {
        var spec = new OrderByIdSpec(orderId);
        var order = await _orderRepository.FirstOrDefaultAsync(spec);

        if (order is null) return NotFound();

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, order, PolicyNames.ResourceOwner);

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        return Ok(order.ToDto());
    }

    [HttpPut("{orderId:int}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<OrderDto>> Update(int orderId, UpdateOrderDto updateOrderDto)
    {
        var spec = new OrderByIdSpec(orderId);
        var order = await _orderRepository.FirstOrDefaultAsync(spec);

        if (order is null) return NotFound();

        order.State = updateOrderDto.State;
        order.UpdateDate = DateTime.UtcNow;

        await _orderRepository.UpdateAsync(order);

        return Ok(order.ToDto());
    }

    [HttpDelete("{orderId:int}")]
    [Authorize(Roles = Roles.User)]
    public async Task<ActionResult> Remove(int orderId)
    {
        var spec = new OrderByIdSpec(orderId);
        var order = await _orderRepository.FirstOrDefaultAsync(spec);

        if (order is null) return NotFound();

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, order, PolicyNames.ResourceOwner);

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        await _orderRepository.DeleteAsync(order);

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create(CreateOrderDto createOrderDto)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (userId is null) return Forbid();

        var priceResponse = await _priceRequestHandler.GetPriceAsync(createOrderDto.ToPriceRequest());

        if (priceResponse is null) return BadRequest();

        var pizzaSpec = new PizzaSizeByIdSpec(createOrderDto.PizzaSize.Id);
        var pizzaSize = await _pizzaSizeRepository.FirstOrDefaultAsync(pizzaSpec);

        if (pizzaSize is null) return NotFound();

        var order = new Order
        {
            CreationDate = DateTime.UtcNow,
            State = createOrderDto.IsDraft ? OrderState.Draft : OrderState.Waiting,
            Price = priceResponse.TotalPrice,
            Size = pizzaSize,
            UserId = userId
        };

        await _orderRepository.AddAsync(order);

        var toppings = createOrderDto.Toppings.FromDto(order.Id);

        order.Toppings = toppings;

        await _orderRepository.UpdateAsync(order);


        return Created("", order.ToDto());
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