namespace Notiom.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notiom.Data;
using Notiom.Dtos;
using Notiom.Models;
using Notiom.Repositories.Tarefas;
using Notiom.Services.Tarefas;
using System.Net.NetworkInformation;
using System.Security.Claims;


[ApiController]
[Route("api/tarefas")]
[Authorize]
public class TarefasController : ControllerBase
{
    private readonly ITarefasService _tarefasService;
    public TarefasController(ITarefasService tarefasService)
    {
        _tarefasService = tarefasService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarTarefasAsync()
    {
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email cannot be null or empty.");
        }
        try
        {
            var tarefas = await _tarefasService.GetAllTarefasAsync(email);
            return Ok(tarefas);
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
            return Problem("An internal error occurred. Please try again later.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CriarTarefaAsync([FromBody] TarefasInputDTO tarefasInputDTO)
    {
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email cannot be null or empty.");
        }
        try
        {
            var tarefaCreated = await _tarefasService.CreateTarefaAsync(tarefasInputDTO, email);
            if (tarefaCreated)
            {
                return Ok("Task created successfully.");
            }
            else
            {
                return BadRequest("Could not create the task.");
            }
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

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarTarefaAsync(int id, [FromBody] TarefasInputDTO tarefasInputDTO)
    {
        try
        {
            var tarefaUpdated = await _tarefasService.UpdateTarefaAsync(tarefasInputDTO, id);
            if (tarefaUpdated)
            {
                return Ok("Task updated successfully.");
            }
            else
            {
                return BadRequest("Could not update the task.");
            }
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarTarefaAsync(int id)
    {
        try
        {
            var tarefaDeleted = await _tarefasService.DeleteTarefaAsync(id);
            if (tarefaDeleted)
            {
                return Ok("Task deleted successfully.");
            }
            else
            {
                return BadRequest("Could not delete the task.");
            }
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