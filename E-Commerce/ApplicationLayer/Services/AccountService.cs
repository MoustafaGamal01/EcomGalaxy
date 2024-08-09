
using Azure.Core;
using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.Domain.Models.User;
using EcomGalaxy.ViewModel.Profile;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Policy;
using System.Text.Encodings.Web;

namespace EcomGalaxy.ApplicationLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
        }


        public async Task<List<string>> ChangePassword(ChangePasswordViewModel changePasswordVM, string UserId)
        {
            List<string> answer = new List<string>();

            ApplicationUser applicationUser = await _userManager.FindByIdAsync(UserId);
            var result = await _userManager.ChangePasswordAsync(applicationUser, changePasswordVM.Password, changePasswordVM.NewPassword);
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

        public async Task<List<string>> ChangeUsername(ChangeUsernameViewModel changeUsernameVM, string UserId)
        {
            List<string> answer = new List<string>();

            ApplicationUser applicationUser1 = await _userManager.FindByNameAsync(changeUsernameVM.NewName);
            if (applicationUser1 != null)
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
