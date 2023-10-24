using Ardalis.Specification;
using PizzaApi.Core.Entities;
using PizzaApi.Core.Enums;
using PizzaApi.Core.Misc;

namespace PizzaApi.Core.Specifications;

public class UserOrdersSpec : Specification<Order>
{
    public UserOrdersSpec(string userId, int page = PaginationHelper.DefaultPage,
        int pageSize = PaginationHelper.DefaultPageSize,
        bool includeDraft = false, bool draftOnly = false)
    {
        Query
            .Where(x => (!includeDraft && !draftOnly
                ? x.State != OrderState.Draft
                : !draftOnly || x.State == OrderState.Draft) && x.UserId == userId)
            .Skip(PaginationHelper.CalculateSkip(pageSize, page))
            .Take(PaginationHelper.CalculateTake(pageSize))
            .Include(x => x.Toppings);
    }
}