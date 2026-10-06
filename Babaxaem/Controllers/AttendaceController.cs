using Babaxaem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Babaxaem.Controllers;

[ApiController]
[Route("api/attendance")]
public class AttendanceController : ControllerBase
{
    private readonly AppDbContext _context;

    public AttendanceController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAttendance(
        [FromQuery] int groupId,
        [FromQuery] DateTime date)
    {
        var result = await _context.Students
            .Where(s => s.GroupId == groupId)
            .Select(s => new
            {
                studentId = s.Id,
                status = _context.Attendance
                    .Where(a => a.StudentId == s.Id &&
                                a.Date.Date == date.Date)
                    .Select(a => a.Status)
                    .FirstOrDefault() ?? "present"
            })
            .ToListAsync();

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetAttendance(AttendanceRequest request)
    {
        var attendance = await _context.Attendance
            .FirstOrDefaultAsync(a =>
                a.StudentId == request.StudentId &&
                a.Date.Date == request.Date.Date);

        if (attendance == null)
        {
            attendance = new Attendance
            {
                StudentId = request.StudentId,
                Date = request.Date.Date,
                Status = request.Status
            };

            _context.Attendance.Add(attendance);
        }
        else
        {
            attendance.Status = request.Status;
        }

        await _context.SaveChangesAsync();

        return Ok(attendance);
    }
}

public class AttendanceRequest
{
    public int StudentId { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } = "present";
}