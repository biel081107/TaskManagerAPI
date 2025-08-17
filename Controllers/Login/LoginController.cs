namespace Notiom.Controllers;

using Microsoft.AspNetCore.Mvc;
using Notiom.Dtos;
using Notiom.Services;


[ApiController]
[Route("api/autenticacao")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly JwtService _jwtService;
    public LoginController(ILoginService loginService, JwtService jwtService)
    {
        _jwtService = jwtService;
        _loginService = loginService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO user)
    {
        if (user == null)
        {
            return BadRequest("User input cannot be null");
        }
        if ( string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
        {
            return BadRequest("Email andPassword fields are required and cannot be empty.");
        }
        try
        {
            var result = await _loginService.LoginAsync(user);

            if (result)
            {
                var token = _jwtService.GerarToken(user.Email, user.Email);
                return Ok(new { message = "Login successful", token = token });
            }
            else
            {
                return Unauthorized("Login failed. Invalid username,email or password.");
            }
        }
        catch (System.ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (System.Exception)
        {
            return Problem("An error occurred while processing your request");
        }

    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserInputDTO user)
    {
        if (user == null)
        {
            return BadRequest("User input cannot be null");
        }
        if (string.IsNullOrEmpty(user.Nome) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Senha))
        {
            return BadRequest("Nome, Email, and Senha fields are required and cannot be empty.");
        }
        try
        {
            var result = await _loginService.RegisterAsync(user);

            if (result)
            {
                return Ok("Registration successful");
            }
            else
            {
                return Conflict("Registration failed. User already exists.");
            }
        }
        catch (System.ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (System.Exception)
        {
            return Problem("An error occurred while processing your request");
        }
    }
}