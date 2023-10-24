using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Core.Auth.Models;
using PizzaApi.Core.DTO;
using PizzaApi.Core.Entities;

namespace PizzaApi.Infrastructure.Data;

public class MainContext : IdentityDbContext<PizzaUser>
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