namespace Notiom.Services;

using Notiom.Dtos;
using Notiom.Repositories;

public class LoginService : ILoginService
{
    private readonly IloginRepository _loginRepository;

    public LoginService(IloginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    public async Task<bool> LoginAsync(UserLoginDTO userInput)
    {
        if (userInput == null)
        {
            throw new ArgumentNullException(nameof(userInput), "User input cannot be null");
        }
        if (string.IsNullOrWhiteSpace(userInput.Email) || string.IsNullOrWhiteSpace(userInput.Password))
        {
            throw new ArgumentException("Email and password cannot be empty");
        }
        try
        {
            return await _loginRepository.LoginAsync(userInput);
        }
        catch (ArgumentException ex)
        {
            // Log the exception (not implemented here)
            throw new InvalidOperationException("An error occurred while logging in", ex);
        }
    }
    public async Task<bool> RegisterAsync(UserInputDTO userInput)
    {
        if (userInput == null)
        {
            throw new ArgumentNullException(nameof(userInput), "User input cannot be null");
        }
        if (string.IsNullOrEmpty(userInput.Nome) || string.IsNullOrWhiteSpace(userInput.Email) || string.IsNullOrWhiteSpace(userInput.Senha))
        {
            throw new ArgumentException("Name, email, and password cannot be empty");
        }
        try
        {
            return await _loginRepository.RegisterAsync(userInput);
        }
        catch (ArgumentException ex)
        {
            // Log the exception (not implemented here)
            throw new InvalidOperationException("An error occurred while registering", ex);
        }
    }
}
