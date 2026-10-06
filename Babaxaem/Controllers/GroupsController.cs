using Babaxaem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Babaxaem.Controllers;

[ApiController]
[Route("api/groups")]
public class GroupsController : ControllerBase
{
    private readonly AppDbContext _context;

    public GroupsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        var groups = await _context.Groups
            .Select(g => new
            {
                id = g.Id,
                name = g.Name
            })
            .ToListAsync();

        return Ok(groups);
    }

    [HttpPost]
    public async Task<IActionResult> AddGroup(Group group)
    {
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();

        return Ok(group);
    }
}