using Babaxaem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Babaxaem.Controllers;

[ApiController]
[Route("api/subjects")]
public class SubjectsController : ControllerBase
{
    private readonly SubjectService _service;

    public SubjectsController(SubjectService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetSubjects()
    {
        var subjects = await _service.GetSubjectsAsync();

        return Ok(subjects.Select(s => new
        {
            id = s.Id,
            name = s.Name
        }));
    }

    [HttpPost]
    public async Task<IActionResult> AddSubject([FromBody] SubjectRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new
            {
                error = "Не указано название предмета"
            });
        }

        var subject = await _service.AddSubjectAsync(request.Name);

        return Ok(new
        {
            id = subject.Id,
            name = subject.Name
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(int id)
    {
        var success = await _service.DeleteSubjectAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}

public class SubjectRequest
{
    public string Name { get; set; } = "";
}