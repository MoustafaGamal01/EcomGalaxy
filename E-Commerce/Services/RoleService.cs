
using EcomGalaxy.Services.IServices;
using EcomGalaxy.ViewModel.Role;
using Microsoft.AspNetCore.Identity;

namespace EcomGalaxy.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<string>> AddRole(RoleViewModel roleVM)
        {
            List<string> answer = new List<string>();

            IdentityRole role = new IdentityRole();
            role.Name = roleVM.RoleName;
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return answer;
            }
            foreach (var item in result.Errors)
            {
                answer.Add(item.Description);
            }
            return answer;
        }
    }
}
