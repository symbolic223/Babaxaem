using Babaxaem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Babaxaem.Controllers;

[ApiController]
[Route("api/attendance")]
public class AttendanceController : ControllerBase
{
    private readonly AttendanceService _service;

    public AttendanceController(AttendanceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAttendance(
        [FromQuery] int groupId,
        [FromQuery] DateTime date)
    {
        var result = await _service.GetAttendanceAsync(
            groupId,
            date
        );

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetAttendance(
        [FromBody] AttendanceRequest request)
    {
        var attendance = await _service.SetAttendanceAsync(
            request.StudentId,
            request.Date,
            request.Status
        );

        return Ok(attendance);
    }
}

public class AttendanceRequest
{
    public int StudentId { get; set; }

    public DateTime Date { get; set; }

    public string Status { get; set; } = "present";
}