using Ardalis.Specification;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Specifications;

public class ToppingByIdSpec : Specification<Topping>, ISingleResultSpecification<Topping>
{
    public ToppingByIdSpec(int categoryId, int toppingId)
    {
        Query
            .Where(x => x.ToppingCategory.Id == categoryId && x.Id == toppingId);
    }
}