using Ardalis.Specification.EntityFrameworkCore;
using PizzaApi.Infrastructure.Interfaces;

namespace PizzaApi.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
    public EfRepository(MainContext dbContext) : base(dbContext)
    {
        
    }
}