using System.ComponentModel.DataAnnotations;

namespace PizzaApi.Core.Auth.Models;

public interface IUserOwnedResource
{
    [Required]
    public string UserId { get; set; }
}