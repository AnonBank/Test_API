
using Microsoft.AspNetCore.Mvc;
using ExamApi.Repository;
using ExamApi.Models;

namespace ExamApi.Controllers
{
    [ApiController]
    [Route("api/approval")]
    public class ApprovalController(IApprovalRepository service) : ControllerBase
    {
        private readonly IApprovalRepository Service = service;

        [HttpPost("list")]
        public async Task<IActionResult> ListApproval([FromBody] RequestListApproval model)
        {
            var result = await Service.ListApproval(model);
            return Ok(result);
        }

        [HttpPost("approve")]
        public async Task<IActionResult>Approve(RequestSaveApproval model)
        {
            await Service.Approve(model);

            return Ok();
        }

        [HttpPost("reject")]
        public async Task<IActionResult> Reject(RequestSaveApproval model)
        {
            await Service.Reject(model);

            return Ok();
        }

    }
}