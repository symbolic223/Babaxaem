using Microsoft.AspNetCore.Mvc;

namespace Babaxaem.Controllers;

[ApiController]
[Route("api/[controller]")] // /api/test
public class TestController : ControllerBase
{
    // GET запрос: /api/test
    [HttpGet]
    public IActionResult GetHello()
    {
        return Ok(new 
        { 
            message = "Я тупая сучка", 
            date = DateTime.Now 
        });
    }
}