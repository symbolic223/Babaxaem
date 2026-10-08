namespace Babaxaem.Models;

public class Student
{
    public int Id { get; set; }

    public string FullName { get; set; } = "";

    public int GroupId { get; set; }

    public Group Group { get; set; } = null!;

    public ICollection<Attendance> Attendance { get; set; } = new List<Attendance>();

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}