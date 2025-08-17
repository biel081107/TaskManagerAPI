using Microsoft.EntityFrameworkCore;
using Notiom.Data;
using Notiom.Dtos;
using Notiom.Models;

namespace Notiom.Repositories;

public class LoginRepository : IloginRepository
{
    private readonly Context _context;
    public LoginRepository(Context context)
    {
        _context = context;
    }

    public async Task<bool> LoginAsync(UserLoginDTO user)
    {
        var email = user.Email.ToLower() ?? string.Empty;
        var password = user.Password ?? string.Empty;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return false; // Invalid input
        }
        var userX = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        var passwordVerify = BCrypt.Net.BCrypt.Verify(password,userX?.Senha);

        if (userX == null)
        {
            return false; // User not found
        }


        var users = await _context.Usuarios.ToListAsync();
        

        return passwordVerify; // Return true if user exists and password matches
    }

    public async Task<bool> RegisterAsync(UserInputDTO user)
    {
        var username = user.Nome.ToLower() ?? string.Empty;
        var email = user.Email.ToLower() ?? string.Empty;
        var password = user.Senha ?? string.Empty;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return false; // Invalid input
        }
        var userX = new Models.Usuarios
        {
            Nome = username,
            Email = email,
            Senha = BCrypt.Net.BCrypt.HashPassword(password),
        };

        try
        {
            _context.Usuarios.Add(userX);

            await _context.SaveChangesAsync();
        }
        catch (System.ArgumentException ex)
        {
            Console.WriteLine($"Error during registration: {ex.Message}");
            return false;
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error during registration: {ex.Message}");
            throw;
        }
        
        

        return true; // Registration successful

        
    }
}