using BarBuddy.Services.Interfaces;
using BarBuddy.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using BarBuddy.Data;

namespace BarBuddy.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVenueSpendRepository _venueSpendRepository;
        private readonly IVenueCheckinRepository _venueCheckinRepository;

        public UserService(IUserRepository userRepository,
                           IVenueSpendRepository venueSpendRepository,
                           IVenueCheckinRepository venueCheckinRepository)
        {
            _userRepository = userRepository;
            _venueSpendRepository = venueSpendRepository;       
            _venueCheckinRepository = venueCheckinRepository;
        }

        public async Task AddSpend(Guid userId, Guid venueId, double amount)
        {
            // check if logged in user has been at this venue on this day
            var venueCheckin = await _venueCheckinRepository.GetUserCheckinByVenueAndDate(userId, venueId, DateTime.Now.Date);

            // if user does not have a check in for this day, create one
            if (venueCheckin == null)
                venueCheckin = await _venueCheckinRepository.CheckinUser(userId, venueId);

            // record how much user spent at venue
            var vsInsert = await _venueSpendRepository.AddVenueSpend(new VenueSpend {
                                                                        VenueCheckinId = venueCheckin.Id,
                                                                        UserId = userId,
                                                                        Amount = amount,
                                                                        CreatedOn = DateTime.UtcNow
                                                                    }
            );
            venueCheckin.VenueSpendIds.Add(vsInsert.Id);

            // add spent amount to venue checkin for today's date
            await _venueCheckinRepository.AddSpend(venueCheckin);
        }

        public async Task CreateUser(string firstName, string lastName, string username, string email, string password)
        {
            await _userRepository.CreateUser(firstName, lastName, username, email, password);
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