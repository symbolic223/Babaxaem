using Babaxaem.Models;
using Microsoft.EntityFrameworkCore;

namespace Babaxaem.Data.Repositories;

public class AttendanceRepository
{
    private readonly AppDbContext _context;

    public AttendanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetStudentsByGroupAsync(int groupId)
    {
        return await _context.Students
            .Where(s => s.GroupId == groupId)
            .ToListAsync();
    }

    public async Task<Attendance?> GetAsync(int studentId, DateTime date)
    {
        return await _context.Attendance
            .FirstOrDefaultAsync(a =>
                a.StudentId == studentId &&
                a.Date.Date == date.Date);
    }

    public async Task<Attendance> AddAsync(Attendance attendance)
    {
        _context.Attendance.Add(attendance);
        await _context.SaveChangesAsync();

        return attendance;
    }

    public async Task UpdateAsync(Attendance attendance)
    {
        await _context.SaveChangesAsync();
    }
}