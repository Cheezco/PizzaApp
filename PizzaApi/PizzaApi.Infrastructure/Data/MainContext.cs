using Microsoft.EntityFrameworkCore;
using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;

namespace PizzaApi.Infrastructure.Data;

public class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<PizzaSize> PizzaSizes { get; set; }
    public DbSet<Topping> Toppings { get; set; }
    public DbSet<SelectedTopping> SelectedToppings { get; set; }
    public DbSet<ToppingCategory> ToppingCategories { get; set; }
}