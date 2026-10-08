using Babaxaem.Data.Repositories;
using Babaxaem.Models;

namespace Babaxaem.Services;

public class SubjectService
{
    private readonly SubjectRepository _repository;

    public SubjectService(SubjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Subject>> GetSubjectsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Subject> AddSubjectAsync(string name)
    {
        var subject = new Subject
        {
            Name = name
        };

        return await _repository.AddAsync(subject);
    }

    public async Task<bool> DeleteSubjectAsync(int id)
    {
        var subject = await _repository.GetByIdAsync(id);

        if (subject == null)
        {
            return false;
        }

        await _repository.DeleteAsync(subject);

        return true;
    }
}