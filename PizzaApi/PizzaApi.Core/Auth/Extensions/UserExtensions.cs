using PizzaApi.Core.Auth.DTO;
using PizzaApi.Core.Auth.Models;

namespace PizzaApi.Core.Auth.Extensions;

public static class UserExtensions
{
    public static UserDto ToDto(this PizzaUser user)
        => new(user.Id, user.UserName, user.Email);
}