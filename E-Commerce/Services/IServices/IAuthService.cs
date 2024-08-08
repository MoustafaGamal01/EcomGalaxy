namespace EcomGalaxy.Services.IServices
{
    public interface IAuthService
    {
        Task<List<string>> Register(CustomerRegisterViewModel customerRegisterVM);

        Task<ResultEnum> Login(LoginViewModel loginVM);

        Task<ResultEnum> Logout();
    }
}
