﻿using BarBuddy.Data;
using BarBuddy.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace BarBuddy.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMongoCollection<ApplicationUser> _usersCollection;

        public UserRepository(IRepository repository,
                              UserManager<ApplicationUser> userManager, 
                              SignInManager<ApplicationUser> signInManager)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
            _usersCollection = _repository.Database.GetCollection<ApplicationUser>("Users");
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

        public async Task<List<ApplicationUser>> ListUsers()
        {
            var users = await _usersCollection.Find(u => true).ToListAsync();
            
            return users;
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