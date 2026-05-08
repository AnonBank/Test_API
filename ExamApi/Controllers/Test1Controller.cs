using ExamApi.Models;
using ExamApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers;

[ApiController]
[Route("api/user")]
public class Test1Controller(ITest1Repository service) : ControllerBase
{
    private readonly ITest1Repository Service = service;

    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] RequestCreateUser model)
    {
        await Service.SaveAsync(model);
        return Ok(model);
    }

    [HttpPost("list")]
    public async Task<IActionResult> ListUser([FromBody] RequestListUser model)
    {
        var result = await Service.ListUser(model);
        return Ok(result);
    }
}