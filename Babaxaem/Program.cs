using Babaxaem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontEnd");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Groups.Any())
    {
        db.Groups.AddRange(
            new Group { Name = "Д-9-15" },
            new Group { Name = "Р-11-15" },
            new Group { Name = "ИДР-11-16" }
        );

        db.Subjects.AddRange(
            new Subject { Name = "Математика" },
            new Subject { Name = "Информатика" },
            new Subject { Name = "Компьютерная графика" },
            new Subject { Name = "Технология печати" }
        );

        db.SaveChanges();
    }
}

app.Run();