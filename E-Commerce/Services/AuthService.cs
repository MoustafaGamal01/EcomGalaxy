using EcomGalaxy.Models.User;
using EcomGalaxy.ViewModel.Auth;
using Microsoft.AspNetCore.Identity;

namespace EcomGalaxy.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<List<string>> Register(CustomerRegisterViewModel customerRegisterVM)
        {
            List<string> answer = new List<string>();

            var isFound = await _userManager.FindByEmailAsync(customerRegisterVM.Email);
            if (isFound != null)
            {
                answer.Add("EmailExists");
                return answer;
            }

            ApplicationUser user = new ApplicationUser();
            user.Name = customerRegisterVM.Name;
            user.UserName = customerRegisterVM.UserName;
            user.Email = customerRegisterVM.Email;
            user.PasswordHash = customerRegisterVM.Password;
            user.City = customerRegisterVM.City;
            user.Country = customerRegisterVM.Country;
            user.PostalCode = customerRegisterVM.PostalCode;
            user.Street = customerRegisterVM.Street;
            var result = await _userManager.CreateAsync(user, customerRegisterVM.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }
            else
            {
                foreach (var res in result.Errors)
                {
                    answer.Add(res.Description);
                }
            }
            return answer;

        }

        public async Task<ResultEnum> Login(LoginViewModel loginVM)
        {
            // check email
            ApplicationUser userModel = await _userManager.FindByEmailAsync(loginVM.Email);

            if (userModel != null)
            {
                // check password
                var validPassword = await _userManager.CheckPasswordAsync(userModel, loginVM.Password);

                if (validPassword)
                {
                    // signIn
                    await _signInManager.SignInAsync(userModel, loginVM.RememberMe);

                    return ResultEnum.Done;
                }

            }
            return ResultEnum.InvalidEmailOrPassword;
        }

        public async Task<ResultEnum> Logout()
        {
            await _signInManager.SignOutAsync();
            return ResultEnum.Done;
        }
    }
}
