using Microsoft.EntityFrameworkCore;
using LimsEmployeService.Models;

namespace LimsEmployeService.Data;

public class PosteContext : DbContext
{
    public PosteContext(DbContextOptions<PosteContext> options) : base(options) { }

    public DbSet<Poste> Postes { get; set; }
    public DbSet<Employe> Employes { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
