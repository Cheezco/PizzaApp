using Ardalis.Specification;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Specifications;

public class ToppingCategoryByNameSpec : Specification<ToppingCategory>, ISingleResultSpecification<ToppingCategory>
{
    public ToppingCategoryByNameSpec(string name)
    {
        Query
            .Where(x => x.Name == name);
    }
}