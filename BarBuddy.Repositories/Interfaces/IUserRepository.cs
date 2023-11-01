using BarBuddy.Data;
using Microsoft.AspNetCore.Identity;

namespace BarBuddy.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(string firstName, string lastName, string username, string email, string password);
        Task<string> LoginUser(string username, string password);
        Task LogoutUser();
        Task<List<ApplicationUser>> ListUsers();
    }
}
