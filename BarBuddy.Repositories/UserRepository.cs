using BarBuddy.Data;
using BarBuddy.Extensions;
using BarBuddy.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarBuddy.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMongoCollection<ApplicationUser> _usersCollection;
        private readonly IVenueCheckinRepository _venueCheckinRepository;

        public UserRepository(IRepository repository,
                              IConfiguration configuration,
                              IVenueCheckinRepository venueCheckinRepository,
                              UserManager<ApplicationUser> userManager, 
                              SignInManager<ApplicationUser> signInManager)
        {
            _repository = repository;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _venueCheckinRepository = venueCheckinRepository;
            _usersCollection = _repository.Database.GetCollection<ApplicationUser>("Users");
        }

        public async Task CreateUser(string firstName, string lastName, string username, string email, string password)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                UserName= username,
                Email= email,
                DOB = DateTime.Now.RandomDOB().Date
            };

            var result = await _userManager.CreateAsync(applicationUser, password);
            await _userManager.AddToRoleAsync(applicationUser, "User");

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().ToString());
        }

        public async Task<List<ApplicationUser>> ListUsers()
        {
            var users = await _usersCollection.Find(u => true).ToListAsync();
            foreach (var user in users)
                user.Checkins = await _venueCheckinRepository.GetUserCheckins(user.Id);

            return users;
        }

        public async Task<string> LoginUser(string username, string password)
        {
            var appUser = await _userManager.FindByEmailAsync(username);
            if (appUser != null)
            {
                var signinResult = await _signInManager.PasswordSignInAsync(appUser, password, false, false);
                if (signinResult.Succeeded)
                {
                    var jwtKey = _configuration.GetValue<string>("JwtKey");
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes(jwtKey);
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Email, username)
                        }),
                        
                        Expires = DateTime.UtcNow.AddHours(1),

                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return tokenHandler.WriteToken(token);
                }
            }

            return string.Empty;
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }
    }
}