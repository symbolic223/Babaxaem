using Babaxaem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Babaxaem.Controllers;

[ApiController]
[Route("api/grades")]
public class GradesController : ControllerBase
{
    private readonly AppDbContext _context;

    public GradesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetGrades([FromQuery] int groupId)
    {
        var students = await _context.Students
            .Where(s => s.GroupId == groupId)
            .ToListAsync();

        var subjects = await _context.Subjects
            .ToListAsync();

        var grades = await _context.Grades
            .Where(g => students.Select(s => s.Id).Contains(g.StudentId))
            .ToListAsync();

        var result = students
            .SelectMany(student => subjects.Select(subject =>
            {
                var grade = grades.FirstOrDefault(g =>
                    g.StudentId == student.Id &&
                    g.SubjectId == subject.Id);

                return new
                {
                    studentId = student.Id,
                    subjectId = subject.Id,
                    value = grade?.Value
                };
            }))
            .ToList();

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetGrade(GradeRequest request)
    {
        var grade = await _context.Grades
            .FirstOrDefaultAsync(g =>
                g.StudentId == request.StudentId &&
                g.SubjectId == request.SubjectId);

        if (grade == null)
        {
            grade = new Grade
            {
                StudentId = request.StudentId,
                SubjectId = request.SubjectId,
                Value = request.Value
            };

            _context.Grades.Add(grade);
        }
        else
        {
            grade.Value = request.Value;
        }

        await _context.SaveChangesAsync();

        return Ok(grade);
    }
}

public class GradeRequest
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int? Value { get; set; }
}