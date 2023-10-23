using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaApi.Core.PriceCalculations;
using PizzaApi.Infrastructure.Data;
using PizzaApi.Infrastructure.Interfaces;
using PizzaApi.Infrastructure.Misc;

namespace PizzaApi.Infrastructure;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<MainContext>(x =>
            x.UseInMemoryDatabase("Main"));

        services
            .AddTransient<PriceRequestHandler>()
            .AddTransient<IPizzaPriceCalculator, PizzaPriceCalculator>()
            .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
            .AddScoped<DbSeeder>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", x =>
            {
                x.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Pagination");
            });
        });
    }
}