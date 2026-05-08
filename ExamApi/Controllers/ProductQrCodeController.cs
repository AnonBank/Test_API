using ExamApi.Models;
using ExamApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers;

[ApiController]
[Route("api/productqr")]
public class ProductQrController(IProductQrRepository service) : ControllerBase
{
    private readonly IProductQrRepository Service = service;

    [HttpPost("list")]
    public async Task<IActionResult> ListProductQr([FromBody] RequestListProductQr model)
    {
        var result = await Service.ListProductQr(model);
        return Ok(result);
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] RequestCreateProductQr model)
    {
        await Service.SaveAsync(model);
        return Ok(model);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] RequestDeleteProductQr model)
    {
        await Service.DeleteAsync(model);
        return Ok(model);
    }
}