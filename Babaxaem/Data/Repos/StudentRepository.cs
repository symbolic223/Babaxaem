using Babaxaem.Models;
using Microsoft.EntityFrameworkCore;

namespace Babaxaem.Data.Repositories;

public class StudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllAsync(int? groupId = null)
    {
        var query = _context.Students.AsQueryable();

        if (groupId.HasValue)
        {
            query = query.Where(s => s.GroupId == groupId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> GroupExistsAsync(int groupId)
    {
        return await _context.Groups
            .AnyAsync(g => g.Id == groupId);
    }

    public async Task<Student> AddAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return student;
    }

    public async Task DeleteAsync(Student student)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
}