using static EcomGalaxy.Services.AccountService;

namespace EcomGalaxy.Services.IServices
{
    public interface IAccountService
    {
        Task<List<string>> AddUser(UserRegisterViewModel userRegisterVM);

        Task<List<string>> Register(CustomerRegisterViewModel customerRegisterVM);

        Task<ResultEnum> Login(LoginViewModel loginVM);

        Task<ResultEnum> Logout();

        Task<List<string>> ChangePassword(ChangePasswordViewModel changePasswordVM, string UserId);

        Task<List<string>> ChangeUsername(ChangeUsernameViewModel changeUsernameVM, string UserId);

		Task<List<string>> ChangeFullName(ChangeFullNameViewModel changeFullNameVM, string UserId);
	}
}
