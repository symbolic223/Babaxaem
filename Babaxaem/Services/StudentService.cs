using Babaxaem.Data.Repositories;
using Babaxaem.Models;

namespace Babaxaem.Services;

public class StudentService
{
    private readonly StudentRepository _repository;

    public StudentService(StudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Student>> GetStudentsAsync(int? groupId)
    {
        return await _repository.GetAllAsync(groupId);
    }

    public async Task<(bool Success, string? Error, Student? Student)> AddStudentAsync(
        string fullName,
        int groupId)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return (false, "Не указано ФИО студента", null);
        }

        if (groupId <= 0)
        {
            return (false, "Не указана группа", null);
        }

        var groupExists = await _repository.GroupExistsAsync(groupId);

        if (!groupExists)
        {
            return (false, $"Группа с ID {groupId} не существует", null);
        }

        var student = new Student
        {
            FullName = fullName,
            GroupId = groupId
        };

        var result = await _repository.AddAsync(student);

        return (true, null, result);
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);

        if (student == null)
        {
            return false;
        }

        await _repository.DeleteAsync(student);

        return true;
    }
}