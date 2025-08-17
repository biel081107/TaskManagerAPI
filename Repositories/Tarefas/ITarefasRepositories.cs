namespace Notiom.Repositories.Tarefas;

using Notiom.Dtos;
using Notiom.Models;
public interface ITarefasRepositories
{
    Task<List<Tarefas>> GetAllTarefasAsync(string userEmail);
    Task<bool> CreateTarefaAsync(Tarefas tarefa);
    Task<bool> UpdateTarefaAsync(TarefasInputDTO tarefa, int id);
    Task<bool> DeleteTarefaAsync(int id);
}