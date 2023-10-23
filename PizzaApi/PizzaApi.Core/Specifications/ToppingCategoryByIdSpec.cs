using Ardalis.Specification;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Specifications;

public class ToppingCategoryByIdSpec : Specification<ToppingCategory>, ISingleResultSpecification<ToppingCategory>
{
    public ToppingCategoryByIdSpec(int id)
    {
        Query
            .Where(x => x.Id == id);
    }
}