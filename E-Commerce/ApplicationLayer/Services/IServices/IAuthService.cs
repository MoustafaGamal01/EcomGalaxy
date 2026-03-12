using EcomGalaxy.Domain.Models;
using EcomGalaxy.ViewModel.Auth;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IAuthService
    {
        Task<List<string>> Register(CustomerRegisterViewModel customerRegisterVM);
        Task<(ResultEnum result, string role)> Login(LoginViewModel loginVM);
        Task<ResultEnum> Logout();
    }
}