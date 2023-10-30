using BarBuddy.Data;
using BarBuddy.Extensions;
using BarBuddy.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace BarBuddy.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository _repository;
        private readonly ICheckinRepository _checkinRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMongoCollection<ApplicationUser> _usersCollection;

        public UserRepository(IRepository repository,
                              ICheckinRepository checkinRepository,
                              UserManager<ApplicationUser> userManager, 
                              SignInManager<ApplicationUser> signInManager)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
            _checkinRepository= checkinRepository;
            _usersCollection = _repository.Database.GetCollection<ApplicationUser>("Users");
        }

        public async Task CheckInToVenue(string username, string venueId)
        {
            var loggedInUser = await _userManager.FindByNameAsync(username);
            var venueCheckin = new VenueCheckin
            {
                UserId = new Guid(loggedInUser.Id.ToString()),
                VenueId = new Guid(venueId),
                CreatedOn= DateTime.Now,
            };

            await _checkinRepository.CheckinUser(venueCheckin);
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
            await _userManager.AddToRoleAsync(applicationUser, "Admin");

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().ToString());
        }

        public async Task<List<ApplicationUser>> ListUsers()
        {
            var users = await _usersCollection.Find(u => true).ToListAsync();
            foreach (var user in users)
                user.Checkins = await _checkinRepository.GetUserCheckins(user.Id);

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