using Babaxaem.Models;
using Microsoft.EntityFrameworkCore;

namespace Babaxaem.Data.Repositories;

public class GradeRepository
{
    private readonly AppDbContext _context;

    public GradeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetStudentsByGroupAsync(int groupId)
    {
        return await _context.Students
            .Where(s => s.GroupId == groupId)
            .ToListAsync();
    }

    public async Task<List<Subject>> GetSubjectsAsync()
    {
        return await _context.Subjects
            .ToListAsync();
    }

    public async Task<List<Grade>> GetGradesForStudentsAsync(
        List<int> studentIds)
    {
        return await _context.Grades
            .Where(g => studentIds.Contains(g.StudentId))
            .ToListAsync();
    }

    public async Task<Grade?> GetAsync(
        int studentId,
        int subjectId)
    {
        return await _context.Grades
            .FirstOrDefaultAsync(g =>
                g.StudentId == studentId &&
                g.SubjectId == subjectId);
    }

    public async Task<Grade> AddAsync(Grade grade)
    {
        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();

        return grade;
    }

    public async Task UpdateAsync(Grade grade)
    {
        await _context.SaveChangesAsync();
    }
}