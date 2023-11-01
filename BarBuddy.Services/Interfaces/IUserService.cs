﻿using BarBuddy.Data;
using Microsoft.AspNetCore.Identity;

namespace BarBuddy.Services.Interfaces
{
    public interface IUserService
    {
        public Task CreateUser(string firstName, string lastName, string username, string email, string password);
        Task<string> LoginUser(string username, string password);
        Task LogoutUser();
        Task<List<ApplicationUser>> ListUsers();
        Task AddSpend(Guid userId, Guid venueId, double amount);
    }
}
