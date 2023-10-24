using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PizzaApi.Core.Auth;
using PizzaApi.Core.Auth.Models;
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
            .AddTransient<IJwtTokenService, JwtTokenService>()
            .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
            .AddScoped<DbSeeder>()
            .AddScoped<AuthDbSeeder>()
            .AddSingleton<IAuthorizationHandler, ResourceOwnerAuthorizationHandler>();

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

        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyNames.ResourceOwner,
                policy => policy.Requirements.Add(new ResourceOwnerRequirement()));
        });

        services
            .AddIdentity<PizzaUser, IdentityRole>()
            .AddEntityFrameworkStores<MainContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters.ValidAudience = configuration["jwt:ValidAudience"];
                options.TokenValidationParameters.ValidIssuer = configuration["jwt:ValidIssuer"];
                options.TokenValidationParameters.IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Secret"] ?? ""));
            });
    }
}