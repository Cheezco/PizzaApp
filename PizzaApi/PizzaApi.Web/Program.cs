using System.IdentityModel.Tokens.Jwt;
using PizzaApi.Infrastructure;
using PizzaApi.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Dependencies.ConfigureServices(builder.Configuration, builder.Services);

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.CreateScope();
var dbSeeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
await dbSeeder.SeedAsync();

var authDbSeeder = scope.ServiceProvider.GetRequiredService<AuthDbSeeder>();
await authDbSeeder.SeedAsync();

app.UseCors("AllowAll");

app.Run();