using Babaxaem.Models;
using Microsoft.EntityFrameworkCore;

namespace Babaxaem.Data.Repositories;

public class GroupRepository
{
    private readonly AppDbContext _context;

    public GroupRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Group>> GetAllAsync()
    {
        return await _context.Groups
            .ToListAsync();
    }

    public async Task<Group?> GetByIdAsync(int id)
    {
        return await _context.Groups
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Group> AddAsync(Group group)
    {
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();

        return group;
    }

    public async Task DeleteAsync(Group group)
    {
        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
    }
}