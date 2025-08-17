namespace Notiom.Services.Usuarios;
using Notiom.Dtos;
using Notiom.Models;


public interface IUsuariosService

{
    public Task<UserOutputDTO> GetPerfilAsync(string email);
    public Task<bool> EditarPerfilAsync(UserInputDTO userInput);
}