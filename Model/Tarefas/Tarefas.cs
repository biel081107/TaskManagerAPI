namespace Notiom.Models;

public class Tarefas
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public int Status { get; set; } // 0: Pendente, 1: Em Progresso, 2: Concluída
    public int UsuarioId { get; set; }
}