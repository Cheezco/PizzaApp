using Ardalis.Specification;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Specifications;

public class PizzaSizeByNameSpec : Specification<PizzaSize>, ISingleResultSpecification<PizzaSize>
{
    public PizzaSizeByNameSpec(string name)
    {
        Query
            .Where(x => x.Name == name);
    }
}