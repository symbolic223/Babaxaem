using Babaxaem.Data.Repositories;
using Babaxaem.Models;

namespace Babaxaem.Services;

public class GroupService
{
    private readonly GroupRepository _repository;

    public GroupService(GroupRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Group>> GetGroupsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Group> AddGroupAsync(string name)
    {
        var group = new Group
        {
            Name = name
        };

        return await _repository.AddAsync(group);
    }

    public async Task<bool> DeleteGroupAsync(int id)
    {
        var group = await _repository.GetByIdAsync(id);

        if (group == null)
        {
            return false;
        }

        await _repository.DeleteAsync(group);

        return true;
    }
}