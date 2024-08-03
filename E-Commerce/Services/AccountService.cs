
using Azure.Core;
using EcomGalaxy.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Policy;
using System.Text.Encodings.Web;

namespace EcomGalaxy.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEmailSender _emailSender;

		public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
			_emailSender = emailSender;
		}

        public async Task<List<string>> AddUser(UserRegisterViewModel userRegisterVM)
        {
            List<string> answer = new List<string>();

            var isFound = await _userManager.FindByEmailAsync(userRegisterVM.Email);
            if (isFound != null)
            {
                answer.Add("EmailExists");
                return answer;
            }

            ApplicationUser user = new ApplicationUser();
            user.Name = userRegisterVM.Name;
            user.UserName = userRegisterVM.UserName;
            user.Email = userRegisterVM.Email;
            user.PasswordHash = userRegisterVM.Password;
            user.City = userRegisterVM.City;
            user.Country = userRegisterVM.Country;
            user.PostalCode = userRegisterVM.PostalCode;
            user.Street = userRegisterVM.Street;
            var result = await _userManager.CreateAsync(user, userRegisterVM.Password);

            if (result.Succeeded)
            {
                // assign role
                await _userManager.AddToRoleAsync(user, userRegisterVM.UserRole);
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

            if(userModel != null)
            {
                // check password
                var validPassword = await _userManager.CheckPasswordAsync(userModel, loginVM.Password);

                if(validPassword)
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

		public async Task<List<string>> ChangePassword(ChangePasswordViewModel changePasswordVM, string UserId)
		{
			List<string> answer = new List<string>();   

			ApplicationUser applicationUser = await _userManager.FindByIdAsync(UserId);
            var result = await _userManager.ChangePasswordAsync(applicationUser ,changePasswordVM.Password, changePasswordVM.NewPassword);
            if(result.Succeeded)
            {
                return answer;
            }
            else
            {
                foreach (var item in result.Errors)
                { 
                    answer.Add(item.Description);
                }
            }
            return answer;
		}

        public async Task<List<string>> ChangeUsername(ChangeUsernameViewModel changeUsernameVM, string UserId)
		{
			List<string> answer = new List<string>();
            
            ApplicationUser applicationUser1 = await _userManager.FindByNameAsync(changeUsernameVM.NewName);
            if(applicationUser1 != null)
			{
				answer.Add("UsernameExists");
				return answer;
			}

			applicationUser1 = await _userManager.FindByIdAsync(UserId);
			applicationUser1.UserName = changeUsernameVM.NewName;
			var result = await _userManager.UpdateAsync(applicationUser1);

			if (result.Succeeded)
			{
				return answer;
			}
			else
			{
				foreach (var item in result.Errors)
				{
					answer.Add(item.Description);
				}
			}
			return answer;
		}

		public async Task<List<string>> ChangeFullName(ChangeFullNameViewModel changeFullName, string UserId)
		{
			// make change full name method implementation
			List<string> answer = new List<string>();
			ApplicationUser applicationUser = await _userManager.FindByIdAsync(UserId);
			applicationUser.Name = changeFullName.NewFullName;
			var result = await _userManager.UpdateAsync(applicationUser);

			if (result.Succeeded)
			{
				return answer;
			}
			else
			{
				foreach (var item in result.Errors)
				{
					answer.Add(item.Description);
				}
			}

			return answer;

		}

        


    }
}
