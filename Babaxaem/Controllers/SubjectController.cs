using Babaxaem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Babaxaem.Controllers;

[ApiController]
[Route("api/subjects")]
public class SubjectsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SubjectsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetSubjects()
    {
        var subjects = await _context.Subjects
            .Select(s => new
            {
                id = s.Id,
                name = s.Name
            })
            .ToListAsync();

        return Ok(subjects);
    }

    [HttpPost]
    public async Task<IActionResult> AddSubject(Subject subject)
    {
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        return Ok(subject);
    }
}