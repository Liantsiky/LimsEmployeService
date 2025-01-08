using Microsoft.EntityFrameworkCore;
using EmployeService.Models;

namespace EmployeService.Data;

public class PosteContext : DbContext
{
    public PosteContext(DbContextOptions<PosteContext> options) : base(options) { }

    public DbSet<Poste> Postes { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
