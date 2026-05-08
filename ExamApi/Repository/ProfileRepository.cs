using ExamApi.Data;
using ExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.Repository
{
    public interface IProfileRepository
    {
        Task<int> SaveAsync(RequestCreateProfile model);
    }

    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;

        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAsync(RequestCreateProfile input)
        {
            try
            {
                var newProfile = new ProfileModel
                {
                    user_index = Guid.NewGuid(),
                    first_name = input.FirstName ?? string.Empty,
                    last_name = input.LastName ?? string.Empty,
                    birthdate = input.BirthDate,
                    email = input.Email,
                    phone = input.Phone,
                    profile = input.Profile,
                    job = input.Job,
                    sex = input.Sex,
                    created_date = DateTime.Now,
                };

                await _context.Profile.AddAsync(newProfile);
                await _context.SaveChangesAsync();

                return newProfile.user_id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}