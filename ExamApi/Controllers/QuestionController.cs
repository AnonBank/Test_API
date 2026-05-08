using ExamApi.Models;
using ExamApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers;

[ApiController]
[Route("api/question")]
public class QuestionController(IQuestionRepository service) : ControllerBase
{
    private readonly IQuestionRepository Service = service;

    [HttpPost("list")]
    public async Task<IActionResult> ListQuestion([FromBody] RequestListQuestion model)
    {
        var result = await Service.ListQuestion(model);
        return Ok(result);
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] RequestCreateQuestion model)
    {
        await Service.SaveAsync(model);
        return Ok(model);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] RequestDeleteQuestion model)
    {
        await Service.DeleteAsync(model);
        return Ok(model);
    }
}