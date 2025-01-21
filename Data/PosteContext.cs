using Microsoft.EntityFrameworkCore;
using LimsEmployeService.Models;

namespace LimsEmployeService.Data;

public class PosteContext : DbContext
{
    public PosteContext(DbContextOptions<PosteContext> options) : base(options) { }

    public DbSet<Poste> Postes { get; set; }
    public DbSet<Employe> Employes { get; set; }
    public DbSet<HistoriqueEmploye> HistoriqueEmployes { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Poste>()
        .HasMany(e => e.Employes)
        .WithOne(e => e.Poste)
        .HasForeignKey(e => e.IdPoste)
        .HasPrincipalKey(e => e.IdPoste);

        modelBuilder.Entity<Departement>()
        .HasMany(e => e.Employes)
        .WithOne(e => e.Departement)
        .HasForeignKey(e => e.IdDepartement)
        .HasPrincipalKey(e => e.IdDepartement);

        modelBuilder.Entity<Employe>()
        .HasMany(e => e.HistoriqueEmployes)
        .WithOne(e => e.Employe)
        .HasForeignKey(e => e.IdEmploye)
        .HasPrincipalKey(e => e.IdEmploye);
    }
}
