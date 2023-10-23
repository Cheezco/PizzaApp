using Ardalis.Specification;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Specifications;

public class PizzaSizeByIdSpec : Specification<PizzaSize>, ISingleResultSpecification<PizzaSize>
{
    public PizzaSizeByIdSpec(int id)
    {
        Query
            .Where(x => x.Id == id);
    }
}