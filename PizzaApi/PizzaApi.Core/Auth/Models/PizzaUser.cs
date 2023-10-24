using Microsoft.AspNetCore.Identity;
using PizzaApi.Core.Entities;

namespace PizzaApi.Core.Auth.Models;

public class PizzaUser : IdentityUser
{
    [PersonalData]
    public List<Order> Orders { get; set; }
}