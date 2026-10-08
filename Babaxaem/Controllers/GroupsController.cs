using Babaxaem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Babaxaem.Controllers;

[ApiController]
[Route("api/groups")]
public class GroupsController : ControllerBase
{
    private readonly GroupService _service;

    public GroupsController(GroupService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        var groups = await _service.GetGroupsAsync();

        return Ok(groups.Select(g => new
        {
            id = g.Id,
            name = g.Name
        }));
    }

    [HttpPost]
    public async Task<IActionResult> AddGroup([FromBody] GroupRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new
            {
                error = "Не указано название группы"
            });
        }

        var group = await _service.AddGroupAsync(request.Name);

        return Ok(new
        {
            id = group.Id,
            name = group.Name
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
        var success = await _service.DeleteGroupAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}

public class GroupRequest
{
    public string Name { get; set; } = "";
}