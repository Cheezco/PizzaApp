using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaApi.Core.Auth;
using PizzaApi.Core.Auth.DTO;
using PizzaApi.Core.Auth.Extensions;
using PizzaApi.Core.Auth.Models;

namespace PizzaApi.Web.Controllers;

[Route("api")]
[AllowAnonymous]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<PizzaUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(UserManager<PizzaUser> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        var user = await _userManager.FindByNameAsync(registerUserDto.UserName);

        if (user is not null) return BadRequest("Request invalid.");

        var newUser = new PizzaUser
        {
            Email = registerUserDto.Email,
            UserName = registerUserDto.UserName
        };

        var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);

        if (!createUserResult.Succeeded)
            return BadRequest("Could not create user.");

        await _userManager.AddToRoleAsync(newUser, Roles.User);

        return CreatedAtAction(nameof(Register), newUser.ToDto());
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserDto loginUserDto)
    {
        var user = await _userManager.FindByNameAsync(loginUserDto.UserName);

        if (user?.UserName is null) return BadRequest("User name or password is invalid.");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

        if (!isPasswordValid) return BadRequest("User name or password is invalid.");

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);

        return Ok(new SuccessfulLoginDto(accessToken));
    }
}