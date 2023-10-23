using Ardalis.Specification;
using PizzaApi.Core.Entities;
using PizzaApi.Core.Misc;

namespace PizzaApi.Core.Specifications;

public class ToppingsSpec : Specification<Topping>
{
    public ToppingsSpec(int categoryId, int page = PaginationHelper.DefaultPage, int pageSize = PaginationHelper.DefaultPageSize)
    {
        Query
            .Where(x => x.ToppingCategory.Id == categoryId)
            .Skip(PaginationHelper.CalculateSkip(pageSize, page))
            .Take(PaginationHelper.CalculateTake(pageSize));
    }
}