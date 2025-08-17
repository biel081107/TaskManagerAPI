

namespace Notiom.Services.Usuarios;

using Notiom.Repositories.Usuarios;
using Notiom.Dtos;
using Notiom.Models;
public class UsuariosService : IUsuariosService
{
    private readonly IUsuariosRepositories _usuariosRepositories;
    public UsuariosService(IUsuariosRepositories usuariosRepositories)
    {
        _usuariosRepositories = usuariosRepositories;
    }
    public async Task<UserOutputDTO> GetPerfilAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email cannot be null or empty.");
        }
        var user = await _usuariosRepositories.GetPerfilAsync(email);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with email {email} not found.");
        }
        return user;
    }
    public async Task<bool> EditarPerfilAsync(UserInputDTO userInput)
    {
        if (userInput == null)
        {
            throw new ArgumentNullException(nameof(userInput), "User input cannot be null.");
        }
        if (string.IsNullOrEmpty(userInput.Email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(userInput.Email));
        }
        if (string.IsNullOrEmpty(userInput.Nome))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(userInput.Nome));
        }
        if (string.IsNullOrEmpty(userInput.Senha))
        {
            throw new ArgumentException("Password cannot be null or empty.", nameof(userInput.Senha));
        }
        
        var user = await _usuariosRepositories.GetUserByEmail(userInput.Email);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with email {userInput.Email} not found.");
        }

        user.Nome = userInput.Nome;
        user.Email = userInput.Email;
        user.Senha = userInput.Senha; // Assuming you want to update the password as well
        
        return await _usuariosRepositories.EditarPerfilAsync(user);
    }
}