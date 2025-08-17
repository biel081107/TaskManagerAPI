namespace Notiom.Services.Tarefas;

using Notiom.Dtos;
using Notiom.Models;
public interface ITarefasService
{
    Task<List<TarefasOutputDTO>> GetAllTarefasAsync(string userEmail);
    Task<bool> CreateTarefaAsync(TarefasInputDTO tarefa, string userEmail);
    Task<bool> UpdateTarefaAsync(TarefasInputDTO tarefa, int idtarefa);
    Task<bool> DeleteTarefaAsync(int id);
}