using System.Text.Json.Serialization;

namespace Babaxaem.Models;

public class Grade
{
    public int StudentId { get; set; }

    [JsonIgnore]
    public Student Student { get; set; } = null!;

    public int SubjectId { get; set; }

    [JsonIgnore]
    public Subject Subject { get; set; } = null!;

    public int? Value { get; set; }
}