using ExamApi.Models;
using ExamApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController(IProductRepository service) : ControllerBase
{
    private readonly IProductRepository Service = service;

    [HttpPost("list")]
    public async Task<IActionResult> ListProductBarcode([FromBody] RequestListProduct model)
    {
        var result = await Service.ListProductBarcode(model);
        return Ok(result);
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] RequestCreateProduct model)
    {
        await Service.SaveAsync(model);
        return Ok(model);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] RequestDeleteProduct model)
    {
        await Service.DeleteAsync(model);
        return Ok(model);
    }
}