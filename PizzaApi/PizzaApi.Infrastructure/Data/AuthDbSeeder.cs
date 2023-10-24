using Microsoft.AspNetCore.Identity;
using PizzaApi.Core.Auth.Models;

namespace PizzaApi.Infrastructure.Data;

public class AuthDbSeeder
{
    private readonly UserManager<PizzaUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthDbSeeder(UserManager<PizzaUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        await AddDefaultRoles();
        await AddAdminUser();
        await AddExampleUser();
    }

    private async Task AddAdminUser()
    {
        var newAdminUser = new PizzaUser()
        {
            UserName = "admin",
            Email = "admin@admin.com",
        };

        var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
        if (existingAdminUser is null)
        {
            var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "!Password123");
            if (createAdminUserResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(newAdminUser, Roles.All);
            }
        }
    }

    private async Task AddExampleUser()
    {
        var newAdminUser = new PizzaUser()
        {
            UserName = "user",
            Email = "user@example.com",
        };

        var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
        if (existingAdminUser is null)
        {
            var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "!Password123");
            if (createAdminUserResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(newAdminUser, new[] { Roles.User });
            }
        }
    }


    private async Task AddDefaultRoles()
    {
        foreach (var role in Roles.All)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}