using System.Text.Json.Serialization;

namespace Babaxaem.Models;

public class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    [JsonIgnore]
    public ICollection<Student> Students { get; set; } = new List<Student>();
}