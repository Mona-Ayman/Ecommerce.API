
namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<UserResultDTO> Login(LoginDTO loginModel);
        public Task<UserResultDTO> Register(RegisterDTO registerModel);
    }
}
