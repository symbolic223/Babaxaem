using Babaxaem.Data;
using Microsoft.EntityFrameworkCore;
using Babaxaem.Data.Repositories;
using Babaxaem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<StudentService>();

builder.Services.AddScoped<GroupRepository>();
builder.Services.AddScoped<GroupService>();

builder.Services.AddScoped<SubjectRepository>();
builder.Services.AddScoped<SubjectService>();

builder.Services.AddScoped<AttendanceRepository>();
builder.Services.AddScoped<AttendanceService>();

builder.Services.AddScoped<GradeRepository>();
builder.Services.AddScoped<GradeService>();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontEnd");

app.UseAuthorization();

app.MapControllers();

app.Run();