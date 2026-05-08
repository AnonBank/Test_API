using ExamApi.Models;
using ExamApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers;

[ApiController]
[Route("api/question-result")]
public class QuestionResultController(IQuestionResultRepository service) : ControllerBase
{
    private readonly IQuestionResultRepository Service = service;

    [HttpPost("list")]
    public async Task<IActionResult> ListQuestionResult([FromBody] RequestListQuestionResult model)
    {
        var result = await Service.ListQuestionResult(model);
        return Ok(result);
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] RequestSubmit model)
    {
        var result = await Service.SubmitExam(model);

        return Ok(result);
    }
}