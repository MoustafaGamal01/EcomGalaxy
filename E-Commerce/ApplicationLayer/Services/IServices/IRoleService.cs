using EcomGalaxy.ViewModel.Role;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IRoleService
    {
        Task<List<string>> AddRole(RoleViewModel roleVM);
    }
}
