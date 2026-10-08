using Babaxaem.Models;
using Microsoft.EntityFrameworkCore;

namespace Babaxaem.Data.Repositories;

public class SubjectRepository
{
    private readonly AppDbContext _context;

    public SubjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Subject>> GetAllAsync()
    {
        return await _context.Subjects
            .ToListAsync();
    }

    public async Task<Subject?> GetByIdAsync(int id)
    {
        return await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Subject> AddAsync(Subject subject)
    {
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        return subject;
    }

    public async Task DeleteAsync(Subject subject)
    {
        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();
    }
}