namespace Notiom.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notiom.Services.Usuarios;
using Notiom.Models;


[ApiController]
[Route("api/usuarios")]
[Authorize]
public class UsuarioController : ControllerBase
{
    private readonly IUsuariosService _usuariosService;
    public UsuarioController(IUsuariosService usuariosService)
    {
        _usuariosService = usuariosService;
    }
    [HttpGet("perfil")]
    public async Task<IActionResult> GetPerfilAsync()
    {
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email cannot be null or empty.");
        }
        try
        {
            var perfil = await _usuariosService.GetPerfilAsync(email);
            return Ok(perfil);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An internal error occurred. Please try again later.");
        }
    }
}