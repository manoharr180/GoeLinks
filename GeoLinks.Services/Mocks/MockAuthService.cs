namespace GeoLinks.Services.Services
{
    using GeoLinks.Entities.Modals;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MockAuthService : IAuthService
    {
        private readonly ConcurrentDictionary<int, ProfileModal> users = new();
        private readonly ConcurrentDictionary<int, (string Otp, DateTime Expiry)> otps = new();
        private int idCounter = 1;

        public MockAuthService()
        {
            // seed a test user
            var u = new ProfileModal
            {
                ProfileId = idCounter,
                UserName = "testuser",
                mailId = "test@example.com",
                PhoneNumber = "1234567890",
                Password = "Password123",
                FName = "Test"
            };
            users.TryAdd(u.ProfileId, u);
            idCounter++;
        }

        public int RegisterUser(ProfileModal profileModal)
        {
            if (users.Values.Any(x => (!string.IsNullOrEmpty(x.mailId) && x.mailId == profileModal.mailId)
                                   || (!string.IsNullOrEmpty(x.PhoneNumber) && x.PhoneNumber == profileModal.PhoneNumber)))
            {
                return 0;
            }

            var id = System.Threading.Interlocked.Increment(ref idCounter);
            profileModal.ProfileId = id;
            users.TryAdd(id, profileModal);
            return id;
        }

        public bool ValidateUser(string mailId, string phoneNumber, string password)
        {
            var user = users.Values.FirstOrDefault(u =>
                (!string.IsNullOrEmpty(mailId) && u.mailId == mailId) ||
                (!string.IsNullOrEmpty(phoneNumber) && u.PhoneNumber == phoneNumber));
            return user != null && user.Password == password;
        }

        public ProfileModal GetProfile(string mailId)
        {
            return users.Values.FirstOrDefault(u => u.mailId == mailId);
        }

        public Task<ProfileModal> FindUserByEmailOrPhoneAsync(string userIdentifier)
        {
            var user = users.Values.FirstOrDefault(u =>
                (!string.IsNullOrEmpty(u.mailId) && u.mailId.Equals(userIdentifier, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(u.PhoneNumber) && u.PhoneNumber.Equals(userIdentifier, StringComparison.OrdinalIgnoreCase)));
            return Task.FromResult(user);
        }

        public Task StoreOtpAsync(int profileId, string otp)
        {
            otps[profileId] = (otp, DateTime.UtcNow.AddMinutes(10));
            return Task.CompletedTask;
        }

        public Task<bool> ValidateOtpAsync(int profileId, string otp)
        {
            if (otps.TryGetValue(profileId, out var entry))
            {
                if (entry.Expiry >= DateTime.UtcNow && entry.Otp == otp)
                {
                    // consume OTP
                    otps.TryRemove(profileId, out _);
                    return Task.FromResult(true);
                }
                // expired or mismatch
            }
            return Task.FromResult(false);
        }

        public Task UpdatePasswordAsync(int profileId, string newPassword)
        {
            if (users.TryGetValue(profileId, out var user))
            {
                user.Password = newPassword;
            }
            return Task.CompletedTask;
        }

        // Additional helpers used by controller flows (if needed elsewhere)
        public ProfileModal GetProfileById(int id)
        {
            users.TryGetValue(id, out var u);
            return u;
        }
    }
}