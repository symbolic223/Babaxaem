using Microsoft.EntityFrameworkCore;
using Babaxaem.Models;

namespace Babaxaem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
}