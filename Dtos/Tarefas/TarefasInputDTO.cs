namespace Notiom.Dtos;

public class TarefasInputDTO
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int State { get; set; }
}