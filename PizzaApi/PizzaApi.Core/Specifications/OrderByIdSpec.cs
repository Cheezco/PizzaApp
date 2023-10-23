using Ardalis.Specification;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Specifications;

public class OrderByIdSpec : Specification<Order>, ISingleResultSpecification<Order>
{
    public OrderByIdSpec(int id)
    {
        Query
            .Where(x => x.Id == id);
    }
}