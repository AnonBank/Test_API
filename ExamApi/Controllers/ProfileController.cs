using ExamApi.Models;
using ExamApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController(IProfileRepository service) : ControllerBase
{
    private readonly IProfileRepository Service = service;

    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] RequestCreateProfile model)
    {
        var userId = await Service.SaveAsync(model);
        return Ok(new
        {
            user_id = userId,
            message = "success"
        });
    }
}