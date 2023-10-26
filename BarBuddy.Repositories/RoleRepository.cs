using BarBuddy.Data;
using BarBuddy.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using System.Xml.Linq;

namespace BarBuddy.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleRepository(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateRole(string name)
        {
            var result = await _roleManager.CreateAsync(new ApplicationRole() { Name = name });

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().ToString());
        }
    }
}