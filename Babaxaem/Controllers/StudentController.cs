using Babaxaem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Babaxaem.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents([FromQuery] int? groupId)
    {
        var query = _context.Students.AsQueryable();

        if (groupId.HasValue)
        {
            query = query.Where(s => s.GroupId == groupId.Value);
        }

        var students = await query
            .Select(s => new
            {
                id = s.Id,
                fullName = s.FullName,
                groupId = s.GroupId
            })
            .ToListAsync();

        return Ok(students);
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] StudentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
        {
            return BadRequest(new
            {
                error = "Не указано ФИО студента"
            });
        }

        if (request.GroupId <= 0)
        {
            return BadRequest(new
            {
                error = "Не указана группа"
            });
        }

        var groupExists = await _context.Groups
            .AnyAsync(g => g.Id == request.GroupId);

        if (!groupExists)
        {
            return BadRequest(new
            {
                error = $"Группа с ID {request.GroupId} не существует"
            });
        }

        var student = new Student
        {
            FullName = request.FullName,
            GroupId = request.GroupId
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            id = student.Id,
            fullName = student.FullName,
            groupId = student.GroupId
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

public class StudentRequest
{
    public string FullName { get; set; } = "";
    public int GroupId { get; set; }
}