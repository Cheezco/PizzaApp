using Ardalis.Specification;
using PizzaApi.Core.Entities;
using PizzaApi.Core.Misc;

namespace PizzaApi.Core.Specifications;

public class PizzaSizesSpec : Specification<PizzaSize>
{
    public PizzaSizesSpec(int page = PaginationHelper.DefaultPage, int pageSize = PaginationHelper.DefaultPageSize)
    {
        Query
            .Skip(PaginationHelper.CalculateSkip(pageSize, page))
            .Take(PaginationHelper.CalculateTake(pageSize));
    }
}