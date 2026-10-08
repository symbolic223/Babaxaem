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

    public async Task<List<Attendance>> GetAttendanceAsync(int groupId, DateTime date)
    {
        var students = await _repository.GetStudentsByGroupAsync(groupId);

        var result = new List<Attendance>();

        foreach (var student in students)
        {
            var attendance = await _repository.GetAsync(student.Id, date);

            if (attendance != null)
            {
                result.Add(attendance);
            }
            else
            {
                result.Add(new Attendance
                {
                    StudentId = student.Id,
                    Date = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc),
                    Status = "absent"
                });
            }
        }

        return result;
    }

    public async Task<Attendance> SetAttendanceAsync(
        int studentId,
        DateTime date,
        string status)
    {
        date = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);

        var existing = await _repository.GetAsync(studentId, date);

        if (existing != null)
        {
            existing.Status = status;

            await _repository.UpdateAsync(existing);

            return existing;
        }

        var attendance = new Attendance
        {
            StudentId = studentId,
            Date = date,
            Status = status
        };

        return await _repository.AddAsync(attendance);
    }
}