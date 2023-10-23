using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaApi.Infrastructure.Data;
using PizzaApi.Infrastructure.Interfaces;

namespace PizzaApi.Infrastructure;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<MainContext>(x =>
            x.UseInMemoryDatabase("Main"));

        services
            .AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

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