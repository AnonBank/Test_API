using ExamApi.Data;
using ExamApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExamApi.Repository
{
    public interface IAuthRepository
    {
        Task Register(RequestRegister model);
        Task<string> Login(RequestLogin model);
    }
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthRepository(
            AppDbContext context,
            IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task Register(RequestRegister model)
        {
            try
            {
                string hash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                var user = new AuthModel
                {
                    user_index = Guid.NewGuid(),
                    username = model.Username,
                    password_hash = hash,
                    created_date = DateTime.Now
                };

                await _context.Auth.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }

        }

        public async Task<string> Login(RequestLogin model)
        {
            var user = _context.Auth
                .FirstOrDefault(x =>
                    x.username == model.Username) ?? throw new Exception("User not found");

            bool ok =
                BCrypt.Net.BCrypt.Verify(
                    model.Password,
                    user.password_hash);

            if (!ok)
                throw new Exception("Password invalid");

            return GenerateJwt(user.username);
        }

        private string GenerateJwt(string username)
        {
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.Name,
                    username)
            };

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _config["Jwt:Key"]));

            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    issuer:
                    _config["Jwt:Issuer"],

                    audience:
                    _config["Jwt:Audience"],

                    claims: claims,

                    expires:
                    DateTime.Now.AddHours(1),

                    signingCredentials:
                    creds
                );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}