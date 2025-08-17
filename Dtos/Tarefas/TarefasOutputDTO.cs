namespace Notiom.Dtos;

public class TarefasOutputDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
}