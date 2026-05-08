using ExamApi.Models;
using ExamApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers;

[ApiController]
[Route("api/queue")]
public class QueueController(IQueueRepository service) : ControllerBase
{
    private readonly IQueueRepository Service = service;

    [HttpGet("next")]
    public async Task<IActionResult> GetNextQueue()
    {
        var result = await Service.GetNextQueue();
        return Ok(new { queue = result });
    }

    [HttpGet("reset")]
    public async Task<IActionResult> ResetQueue()
    {
        var result = await Service.ResetQueue();
        return Ok(new { queue = result });
    }

    [HttpGet("check-queue")]
    public async Task<IActionResult> CheckQueue()
    {
        var result = await Service.CheckQueue();
        return Ok(new { queue = result });
    }
}