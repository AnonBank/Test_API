using ExamApi.Models;
using ExamApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers;

[ApiController]
[Route("api/text")]
public class TextController(ITextRepository service) : ControllerBase
{
    private readonly ITextRepository Service = service;

    [HttpPost("get")]
    public async Task<IActionResult> GetText([FromBody] RequestText model)
    {
        var result = await Service.GetText(model);
        return Ok(result);
    }
}