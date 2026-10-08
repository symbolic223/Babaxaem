namespace Babaxaem.Models;

public class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}