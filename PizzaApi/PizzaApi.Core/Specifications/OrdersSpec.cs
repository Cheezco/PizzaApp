using Ardalis.Specification;
using PizzaApi.Core.Entities;
using PizzaApi.Core.Enums;
using PizzaApi.Core.Misc;

namespace PizzaApi.Core.Specifications;

public class OrdersSpec : Specification<Order>
{
    public OrdersSpec(int page = PaginationHelper.DefaultPage, int pageSize = PaginationHelper.DefaultPageSize,
        bool includeDraft = false, bool draftOnly = false)
    {
        Query
            .Where(x => !includeDraft && !draftOnly
                ? x.State != OrderState.Draft
                : !draftOnly || x.State == OrderState.Draft)
            .Skip(PaginationHelper.CalculateSkip(pageSize, page))
            .Take(PaginationHelper.CalculateTake(pageSize));
    }
}