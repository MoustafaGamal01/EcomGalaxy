namespace EcomGalaxy.Services.IServices
{
    public interface IRoleService
    {
        Task<List<string>> AddRole(RoleViewModel roleVM);
    }
}
