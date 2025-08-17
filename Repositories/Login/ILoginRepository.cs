using Notiom.Dtos;

namespace Notiom.Repositories;

public interface IloginRepository
{
    Task<bool> LoginAsync(UserLoginDTO userInput);
    Task<bool> RegisterAsync(UserInputDTO userInput);   
}