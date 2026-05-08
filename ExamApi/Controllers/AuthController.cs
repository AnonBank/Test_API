using ExamApi.Models;
using ExamApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(
        IAuthRepository service
    ) : ControllerBase
    {
        private readonly IAuthRepository Service = service;

        [HttpPost("register")]
        public async Task<IActionResult>Register(RequestRegister model)
        {
            try
            {
                await Service.Register(model);
                return Ok(new { message = "สมัครสมาชิกสำเร็จ" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult>Login(RequestLogin model)
        {
            var token = await Service.Login(model);

            return Ok(new
            {
                token = token
            });
        }
    }
}