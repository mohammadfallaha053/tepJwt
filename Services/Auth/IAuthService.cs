using JWT53.Dto.Auth;

namespace JWT53.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterAsync(Register model);
        Task<AuthDto> GetTokenAsync(Login model);

    }
}