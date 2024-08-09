using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.Domain.Models.User;
using EcomGalaxy.ViewModel.Auth;
using Microsoft.AspNetCore.Identity;

namespace EcomGalaxy.ApplicationLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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

    }
}
