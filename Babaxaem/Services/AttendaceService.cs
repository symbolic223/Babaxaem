using Babaxaem.Data.Repositories;
using Babaxaem.Models;

namespace Babaxaem.Services;

public class AttendanceService
{
    private readonly AttendanceRepository _repository;

    public AttendanceService(AttendanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<AttendanceResult>> GetAttendanceAsync(
        int groupId,
        DateTime date)
    {
        var students = await _repository.GetStudentsByGroupAsync(groupId);

        var result = new List<AttendanceResult>();

        foreach (var student in students)
        {
            var attendance = await _repository.GetAsync(
                student.Id,
                date
            );

            result.Add(new AttendanceResult
            {
                StudentId = student.Id,
                Status = attendance?.Status ?? "present"
            });
        }

        return result;
    }

    public async Task<Attendance> SetAttendanceAsync(
        int studentId,
        DateTime date,
        string status)
    {
        var attendance = await _repository.GetAsync(
            studentId,
            date
        );

        if (attendance == null)
        {
            attendance = new Attendance
            {
                StudentId = studentId,
                Date = date.Date,
                Status = status
            };

            return await _repository.AddAsync(attendance);
        }

        attendance.Status = status;

        await _repository.UpdateAsync(attendance);

        return attendance;
    }
}

public class AttendanceResult
{
    public int StudentId { get; set; }

    public string Status { get; set; } = "";
}