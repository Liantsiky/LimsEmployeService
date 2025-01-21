using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using LimsEmployeService.Data;
using LimsEmployeService.Service;

namespace LimsEmployeService.Models;
[Table("Employe")]
public class Employe
{
    public async Task<Employe> Insert(PosteContext dbContext, EmployeService employeService)
    {
        Employe result = new Employe();
        using(var transaction = await dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                dbContext.Employes.Add(this);
                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            result = await employeService.GetEmploye(this.IdEmploye);
        }

        return result;
    }

    [Key]
    [Column("id_employe")]
    public int IdEmploye { get; set; }
    [Column("matricule")]
    public string? Matricule { get; set; }
    [Column("nom")]
    public string? Nom { get; set; }
    [Column("prenom")]
    public string? Prenom { get; set; }
    [Column("genre")]
    public int Genre { get; set; }
    [Column("cin")]
    public string? Cin { get; set; }
    [Column("contact")]
    public string? Contact { get; set; }
    [Column("adresse")]
    public string? Adresse { get; set; }
    [Column("manager")]
    public string? Manager { get; set; }
    [Column("id_departement")]
    public int? IdDepartement { get; set; }
    [ForeignKey("IdDepartement")]
    public Departement? Departement { get; set; }
    [Column("id_poste")] 
    public int IdPoste { get; set; }
    [ForeignKey("IdPoste")]
    public Poste? Poste { get; set; }

    public ICollection<HistoriqueEmploye>? HistoriqueEmployes { get; set; }

}
