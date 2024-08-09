using EcomGalaxy.ViewModel.Profile;
using static EcomGalaxy.ApplicationLayer.Services.AccountService;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IAccountService
    {

        Task<List<string>> ChangePassword(ChangePasswordViewModel changePasswordVM, string UserId);

        Task<List<string>> ChangeUsername(ChangeUsernameViewModel changeUsernameVM, string UserId);

        Task<List<string>> ChangeFullName(ChangeFullNameViewModel changeFullNameVM, string UserId);
    }
}
