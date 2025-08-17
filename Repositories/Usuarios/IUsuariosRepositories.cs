using Notiom.Dtos;

namespace Notiom.Repositories.Usuarios;

public interface IUsuariosRepositories
{
    Task<UserInputDTO> GetUserByEmail(string email);
    Task<UserOutputDTO> GetPerfilAsync(string email);
    Task<bool> EditarPerfilAsync(UserInputDTO userInput);

}