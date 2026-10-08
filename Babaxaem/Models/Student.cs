using System.Text.Json.Serialization;

namespace Babaxaem.Models;

public class Student
{
    public int Id { get; set; }

    public string FullName { get; set; } = "";

    public int GroupId { get; set; }

    [JsonIgnore]
    public Group Group { get; set; } = null!;

    [JsonIgnore]
    public ICollection<Attendance> Attendance { get; set; } = new List<Attendance>();

    [JsonIgnore]
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}