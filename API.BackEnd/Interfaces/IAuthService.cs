using API.BackEnd.DTOS.Auth;

namespace API.BackEnd.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(RegistroDto dto);
        Task<AuthResultDto> LoginAsync(LoginDto dto);
    }
}
