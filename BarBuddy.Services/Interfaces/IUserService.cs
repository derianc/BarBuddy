using BarBuddy.Data;
using Microsoft.AspNetCore.Identity;

namespace BarBuddy.Services.Interfaces
{
    public interface IUserService
    {
        public Task CreateUser(string name, string email, string password);
        Task<SignInResult> LoginUser(string username, string password);
        Task LogoutUser();
        Task<List<ApplicationUser>> ListUsers();
    }
}
