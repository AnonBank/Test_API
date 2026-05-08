using ExamApi.Data;
using ExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.Repository
{
    public interface ITest1Repository
    {
        Task SaveAsync(RequestCreateUser model);
        Task<ListUser> ListUser(RequestListUser model);
    }

    public class Test1Repository : ITest1Repository
    {
        private readonly AppDbContext _context;

        public Test1Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(RequestCreateUser input)
        {
            try
            {
                var newUser = new UserModel
                {
                    user_index = Guid.NewGuid(),
                    first_name = input.FirstName ?? string.Empty,
                    last_name = input.LastName ?? string.Empty,
                    birthdate = input.BirthDate,
                    age = input.Age,
                    address = input.Address ?? string.Empty,
                    created_date = DateTime.Now,
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ListUser> ListUser(RequestListUser input)
        {
            try
            {
                var users = _context.Users
                    .AsNoTracking()
                    .ToList();

                var query = users.Select(w => new ListUser.UserDetail
                {
                    UserIndex = w.user_index,
                    FirstName = w.first_name, 
                    LastName = w.last_name,
                    Age = w.age,
                    Address = w.address,
                    BirthDate = w.birthdate.HasValue? w.birthdate.Value.ToString("dd/MM/yyyy"): "",
                    CreatedDate = w.created_date
                }).OrderBy(o => o.CreatedDate).ToList();

                return await Task.FromResult(new ListUser
                {
                    Details = query
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}