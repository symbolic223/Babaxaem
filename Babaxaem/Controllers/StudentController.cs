using Babaxaem.Models;
using Babaxaem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Babaxaem.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly StudentService _service;

    public StudentsController(StudentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents([FromQuery] int? groupId)
    {
        var students = await _service.GetStudentsAsync(groupId);

        return Ok(students.Select(s => new
        {
            id = s.Id,
            fullName = s.FullName,
            groupId = s.GroupId
        }));
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] StudentRequest request)
    {
        var result = await _service.AddStudentAsync(
            request.FullName,
            request.GroupId
        );

        if (!result.Success)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return Ok(new
        {
            id = result.Student!.Id,
            fullName = result.Student.FullName,
            groupId = result.Student.GroupId
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var success = await _service.DeleteStudentAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}

public class StudentRequest
{
    public string FullName { get; set; } = "";
    public int GroupId { get; set; }
}