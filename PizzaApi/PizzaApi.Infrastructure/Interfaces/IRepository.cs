using Ardalis.Specification;

namespace PizzaApi.Infrastructure.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
    
}