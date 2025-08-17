namespace Notiom.Services.Tarefas;

using Microsoft.EntityFrameworkCore;
using Notiom.Data;
using Notiom.Dtos;
using Notiom.Models;
using Notiom.Repositories.Tarefas;
public class TarefasService : ITarefasService
{
    private readonly ITarefasRepositories _tarefasRepositories;
    private readonly Context context;

    public TarefasService(ITarefasRepositories tarefasRepositories, Context _context)
    {
        _tarefasRepositories = tarefasRepositories;
        context = _context;
    }

    public async Task<List<TarefasOutputDTO>> GetAllTarefasAsync(string userEmail)
    {
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new ArgumentException("Email cannot be null or empty.");
        }
        var tarefas = await _tarefasRepositories.GetAllTarefasAsync(userEmail);
        if (tarefas == null || tarefas.Count == 0)
        {
            return new List<TarefasOutputDTO>();
        }
        var tarefasOutput = new List<TarefasOutputDTO>();
        foreach (var tarefa in tarefas)
        {
            var tarefaOutput = new TarefasOutputDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                CreateDate = tarefa.DataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                UserEmail = userEmail
            };
            if (tarefa.Status == 0)
            {
                tarefaOutput.State = "Pending";
            }
            else if (tarefa.Status == 1)
            {
                tarefaOutput.State = "In Progress";
            }
            else if (tarefa.Status == 2)
            {
                tarefaOutput.State = "Concluded";
            }
            tarefasOutput.Add(tarefaOutput);
        }
        return tarefasOutput;
        
    }


    public async Task<bool> CreateTarefaAsync(TarefasInputDTO tarefa, string userEmail)
    {
        if (tarefa == null)
        {
            throw new ArgumentNullException(nameof(tarefa));
        }
        if (string.IsNullOrEmpty(tarefa.Titulo)
            || string.IsNullOrEmpty(tarefa.Descricao)
            || tarefa.State < 0
            || tarefa.State > 2)
        {
            throw new ArgumentException("Invalid tarefa data.");
        }
        var user = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == userEmail);
        if (user == null)
        {
            throw new ArgumentException("User not found.");
        }

        var newTarefa = new Models.Tarefas
        {
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            Status = tarefa.State,
            DataCriacao = DateTime.Now,
            UsuarioId = user.Id
        };
        return await _tarefasRepositories.CreateTarefaAsync(newTarefa);
    }

    public async Task<bool> UpdateTarefaAsync(TarefasInputDTO tarefa, int idtarefa)
    {
        if (tarefa == null)
        {
            throw new ArgumentNullException(nameof(tarefa));
        }
        if (string.IsNullOrEmpty(tarefa.Titulo)
            || string.IsNullOrEmpty(tarefa.Descricao)
            || tarefa.State < 0
            || tarefa.State > 2)
        {
            throw new ArgumentException("Invalid tarefa data.");
        }
        if (idtarefa <= 0)
        {
            throw new ArgumentException("Invalid tarefa ID.");
        }
        return await _tarefasRepositories.UpdateTarefaAsync(tarefa, idtarefa);
    }

    public async Task<bool> DeleteTarefaAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid tarefa ID.");
        }
        return await _tarefasRepositories.DeleteTarefaAsync(id);
    }
}