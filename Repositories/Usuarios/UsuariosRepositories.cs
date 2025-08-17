using Microsoft.EntityFrameworkCore;
using Notiom.Data;
using Notiom.Dtos;

namespace Notiom.Repositories.Usuarios;

public class UsuariosRepositories : IUsuariosRepositories
{
    private readonly Context _context;
    public UsuariosRepositories(Context context)
    {
        _context = context;
    }
    
    public async Task<UserInputDTO> GetUserByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email cannot be null or empty.");
        }

        var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with email {email} not found.");
        }

        return new UserInputDTO
        {
            Email = user.Email,
            Nome = user.Nome,
            Senha = user.Senha
        };
    }

    public async Task<UserOutputDTO> GetPerfilAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email cannot be null or empty.");
        }

        var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            throw new ArgumentException("User not found.");
        }
        return new UserOutputDTO
        {
            Email = user.Email,
            Name = user.Nome,
            CreateDate = user.DataCriacao.ToString("yyyy-MM-dd")
        };

    }
    public async Task<bool> EditarPerfilAsync(UserInputDTO user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User input cannot be null.");
        }

        var userX = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == user.Email);

        if (userX == null)
        {
            throw new ArgumentException("User not found.");
        }

        userX.Nome = user.Nome;
        userX.Email = user.Email;
        userX.Senha = BCrypt.Net.BCrypt.HashPassword(user.Senha);

        _context.Usuarios.Update(userX);

        await _context.SaveChangesAsync();

        return true;
    }

}