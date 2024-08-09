using EcomGalaxy.ViewModel.Auth;

namespace EcomGalaxy.Services.IServices
{
    public interface IUserService
    {
        Task<List<string>> AddUser(UserRegisterViewModel userRegisterVM);

    }
}
