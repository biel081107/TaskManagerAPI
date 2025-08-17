using Notiom.Dtos;
using Notiom.Repositories;

namespace  Notiom.Services;

public interface ILoginService
{
    public Task<bool> LoginAsync(UserLoginDTO userInput);
    public Task<bool> RegisterAsync(UserInputDTO userInput);
    
}