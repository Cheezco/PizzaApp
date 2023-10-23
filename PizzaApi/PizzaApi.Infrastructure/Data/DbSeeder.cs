using PizzaApi.Core.Entities;
using PizzaApi.Core.Specifications;
using PizzaApi.Infrastructure.Interfaces;

namespace PizzaApi.Infrastructure.Data;

public class DbSeeder
{
    private readonly IRepository<ToppingCategory> _toppingCategoryRepository;
    private readonly IRepository<PizzaSize> _pizzaSizeRepository;

    public DbSeeder(IRepository<ToppingCategory> toppingCategoryRepository, IRepository<PizzaSize> pizzaSizeRepository)
    {
        _toppingCategoryRepository = toppingCategoryRepository;
        _pizzaSizeRepository = pizzaSizeRepository;
    }

    public async Task SeedAsync()
    {
        await AddPizzaSizesAsync();
        await AddToppingCategoriesAsync();
    }

    private async Task AddPizzaSizesAsync()
    {
        var sizes = new List<PizzaSize>
        {
            new() { Name = "Small", Price = 8, Order = 1 },
            new() { Name = "Medium", Price = 10, Order = 2 },
            new() { Name = "Large", Price = 12, Order = 3 },
        };

        foreach (var size in sizes)
        {
            var spec = new PizzaSizeByNameSpec(size.Name);
            var existingPizzaSize = await _pizzaSizeRepository.FirstOrDefaultAsync(spec);

            if (existingPizzaSize is not null) continue;

            await _pizzaSizeRepository.AddAsync(size);
        }
    }

    private async Task AddToppingCategoriesAsync()
    {
        var meatCategory = new ToppingCategory
        {
            Name = "Meat",
            Toppings = new List<Topping>
            {
                new()
                {
                    Name = "Bacon",
                    Limit = 3,
                    Price = 1
                },
                new()
                {
                    Name = "Beef",
                    Limit = 3,
                    Price = 1
                },
                new()
                {
                    Name = "Chicken",
                    Limit = 3,
                    Price = 1
                },
            }
        };

        var cheeseCategory = new ToppingCategory
        {
            Name = "Cheese",
            Toppings = new List<Topping>
            {
                new()
                {
                    Name = "Cheddar",
                    Limit = 4,
                    Price = 1
                },
                new()
                {
                    Name = "Feta",
                    Limit = 2,
                    Price = 1
                },
                new()
                {
                    Name = "Mozzarella",
                    Limit = 3,
                    Price = 1
                },
            }
        };

        var vegetableCategory = new ToppingCategory
        {
            Name = "Vegetables",
            Toppings = new List<Topping>
            {
                new()
                {
                    Name = "Black Olives",
                    Limit = 3,
                    Price = 1
                },
                new()
                {
                    Name = "Jalapeno",
                    Limit = 2,
                    Price = 1
                },
                new()
                {
                    Name = "Pickles",
                    Limit = 3,
                    Price = 1
                },
            }
        };

        var categories = new List<ToppingCategory>
        {
            meatCategory,
            cheeseCategory,
            vegetableCategory
        };

        foreach (var category in categories)
        {
            var spec = new ToppingCategoryByNameSpec(category.Name);
            var existingCategory = await _toppingCategoryRepository.FirstOrDefaultAsync(spec);

            if (existingCategory is not null) continue;

            await _toppingCategoryRepository.AddAsync(category);
        }
    }
}