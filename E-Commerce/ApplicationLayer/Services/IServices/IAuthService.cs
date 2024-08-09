using EcomGalaxy.Domain.Models;
using EcomGalaxy.ViewModel.Auth;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IAuthService
    {
        Task<List<string>> Register(CustomerRegisterViewModel customerRegisterVM);

        Task<ResultEnum> Login(LoginViewModel loginVM);

        Task<ResultEnum> Logout();
    }
}
