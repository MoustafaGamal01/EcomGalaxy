using EcomGalaxy.ViewModel.Auth;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IUserService
    {
        Task<List<string>> AddUser(UserRegisterViewModel userRegisterVM);

    }
}
