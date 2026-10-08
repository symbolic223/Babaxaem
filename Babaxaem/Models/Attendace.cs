namespace Babaxaem.Models;

public class Attendance
{
    public int StudentId { get; set; }

    public Student Student { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Status { get; set; } = "present";
}