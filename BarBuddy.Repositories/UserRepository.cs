using BarBuddy.Data;
using BarBuddy.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace BarBuddy.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task CreateUser(string name, string email, string password)
        {
            var applicationUser = new ApplicationUser
            {
                UserName= name,
                Email= email,
            };

            var result = await _userManager.CreateAsync(applicationUser, password);
            await _userManager.AddToRoleAsync(applicationUser, "Admin");

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().ToString());
        }

        public async Task<ApplicationUser> ListUsers()
        {
            return await Task.FromResult(new ApplicationUser());
        }

        public async Task<SignInResult> LoginUser(string username, string password)
        {
            var appUser = await _userManager.FindByEmailAsync(username);
            if (appUser != null)
                return await _signInManager.PasswordSignInAsync(appUser, password, false, false);
            
            return new SignInResult();
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }
    }
}