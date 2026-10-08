using Babaxaem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Babaxaem.Controllers;

[ApiController]
[Route("api/grades")]
public class GradesController : ControllerBase
{
    private readonly GradeService _service;

    public GradesController(GradeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetGrades(
        [FromQuery] int groupId)
    {
        var grades = await _service.GetGradesAsync(groupId);

        return Ok(grades);
    }

    [HttpPut]
    public async Task<IActionResult> SetGrade(
        [FromBody] GradeRequest request)
    {
        var grade = await _service.SetGradeAsync(
            request.StudentId,
            request.SubjectId,
            request.Value
        );

        return Ok(grade);
    }
}

public class GradeRequest
{
    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public int? Value { get; set; }
}