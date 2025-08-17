namespace Notiom.Repositories.Tarefas;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notiom.Data;
using Notiom.Dtos;
using Notiom.Repositories.Usuarios;
public class TarefasRepositories : ITarefasRepositories
{
    private readonly Context _context;

    public TarefasRepositories(Context context)
    {
        _context = context;
    }

    public async Task<List<Models.Tarefas>> GetAllTarefasAsync(string userEmail)
    {
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new ArgumentException("Email cannot be null or empty.");
        }

        var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user == null)
        {
            throw new ArgumentException("User not found.");
        }
        return await _context.Tarefas.Where(t => t.UsuarioId == user.Id).ToListAsync();
    }
    public async Task<bool> CreateTarefaAsync(Models.Tarefas tarefa)
    {
        if (tarefa == null)
        {
            throw new ArgumentNullException(nameof(tarefa));
        }
        try
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public async Task<bool> UpdateTarefaAsync(TarefasInputDTO tarefa, int id)
    {
        if (tarefa == null)
        {
            throw new ArgumentNullException(nameof(tarefa));
        }
        var existingTarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
        if (existingTarefa == null)
        {
            return false; // Tarefa not found
        }
        existingTarefa.Titulo = tarefa.Titulo;
        existingTarefa.Descricao = tarefa.Descricao;
        existingTarefa.Status = tarefa.State;
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteTarefaAsync(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null)
        {
            return false; // Tarefa not found
        }
        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return true;
    }
}