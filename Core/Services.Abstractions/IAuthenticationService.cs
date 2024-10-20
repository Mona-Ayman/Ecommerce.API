
namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<UserResultDTO> Login(LoginDTO loginModel);
        public Task<UserResultDTO> Register(RegisterDTO registerModel);
        public Task<UserResultDTO> GetUserByEmail(string email);
        public Task<bool> CheckEmailExist(string email);
        public Task<AddressDTO> GetUserAddress(string email);
        public Task<AddressDTO> UpdateUserAddress(AddressDTO address, string email);
    }
}
