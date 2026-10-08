using Babaxaem.Data.Repositories;
using Babaxaem.Models;

namespace Babaxaem.Services;

public class GradeService
{
    private readonly GradeRepository _repository;

    public GradeService(GradeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GradeResult>> GetGradesAsync(int groupId)
    {
        var students = await _repository.GetStudentsByGroupAsync(groupId);
        var subjects = await _repository.GetSubjectsAsync();

        var studentIds = students
            .Select(s => s.Id)
            .ToList();

        var grades = await _repository.GetGradesForStudentsAsync(
            studentIds
        );

        var result = new List<GradeResult>();

        foreach (var student in students)
        {
            foreach (var subject in subjects)
            {
                var grade = grades.FirstOrDefault(g =>
                    g.StudentId == student.Id &&
                    g.SubjectId == subject.Id);

                result.Add(new GradeResult
                {
                    StudentId = student.Id,
                    SubjectId = subject.Id,
                    Value = grade?.Value
                });
            }
        }

        return result;
    }

    public async Task<Grade> SetGradeAsync(
        int studentId,
        int subjectId,
        int? value)
    {
        var grade = await _repository.GetAsync(
            studentId,
            subjectId
        );

        if (grade == null)
        {
            grade = new Grade
            {
                StudentId = studentId,
                SubjectId = subjectId,
                Value = value
            };

            return await _repository.AddAsync(grade);
        }

        grade.Value = value;

        await _repository.UpdateAsync(grade);

        return grade;
    }
}

public class GradeResult
{
    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public int? Value { get; set; }
}