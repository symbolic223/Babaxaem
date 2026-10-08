using System.Text.Json.Serialization;

namespace Babaxaem.Models;

public class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    [JsonIgnore]
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}