using Microsoft.EntityFrameworkCore;

namespace Babaxaem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Attendance> Attendance => Set<Attendance>();
    public DbSet<Grade> Grades => Set<Grade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Group>()
            .HasKey(g => g.Id);

        modelBuilder.Entity<Subject>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Student>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Attendance>()
            .HasKey(a => new { a.StudentId, a.Date });

        modelBuilder.Entity<Grade>()
            .HasKey(g => new { g.StudentId, g.SubjectId });

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Group)
            .WithMany(g => g.Students)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Student)
            .WithMany(s => s.Attendance)
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Subject)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public ICollection<Student> Students { get; set; } = new List<Student>();
}

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";

    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public ICollection<Attendance> Attendance { get; set; } = new List<Attendance>();
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}

public class Attendance
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Status { get; set; } = "present";
}

public class Grade
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public int? Value { get; set; }
}