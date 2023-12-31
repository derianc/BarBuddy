﻿using BarBuddy.Services.Interfaces;
using BarBuddy.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using BarBuddy.Data;

namespace BarBuddy.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUser(string name, string email, string password)
        {
            await _userRepository.CreateUser(name, email, password);
        }

        public async Task<List<ApplicationUser>> ListUsers()
        {
            return await _userRepository.ListUsers();
        }

        public async Task<SignInResult> LoginUser(string username, string password)
        {
            return await _userRepository.LoginUser(username, password);
        }

        public async Task LogoutUser()
        {
            await _userRepository.LogoutUser();
        }

        
    }
}