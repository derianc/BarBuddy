using BarBuddy.Repositories.Interfaces;
using BarBuddy.Services.Interfaces;

namespace BarBuddy.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task CreateRole(string name)
        {
            await _roleRepository.CreateRole(name);
        }
    }
}