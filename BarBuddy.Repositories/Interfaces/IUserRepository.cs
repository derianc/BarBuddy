using BarBuddy.Data;
using Microsoft.AspNetCore.Identity;

namespace BarBuddy.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(string name, string email, string password);
        Task<SignInResult> LoginUser(string username, string password);
        Task LogoutUser();
        Task<List<ApplicationUser>> ListUsers();
    }
}
