using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using LimsEmployeService.Data;
using LimsEmployeService.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LimsEmployeService.Models;
[Table("Employe")]
public class Employe
{

    private async Task<Employe> DtoToEmploye(EmployeDto employeDto)
    {
        Employe result = new Employe();
        string dtoAsJson = JsonSerializer.Serialize(employeDto);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        result = JsonSerializer.Deserialize<Employe>(dtoAsJson, options);
        return result;
    }

    public HistoriqueEmploye GetLastPoste()
    {
        HistoriqueEmploye result = null;
        int lastIndex = this.HistoriqueEmployes.Count - 1;
        result = this.HistoriqueEmployes.ElementAt(lastIndex);
        return result;
    }

    public async Task<Employe> HandleDtoForUpdate(EmployeDto employe, PosteContext dbContext)
    {
        Employe result = new Employe();
        result = await DtoToEmploye(employe);
        result.HistoriqueEmployes = await dbContext.HistoriqueEmployes
            .Where(h => h.IdEmploye == result.IdEmploye)
            .ToListAsync();

        HistoriqueEmploye lastPoste = result.GetLastPoste();
        lastPoste.DateFin = employe.DateFinPoste;
        HistoriqueEmploye newPoste = new HistoriqueEmploye();
        newPoste.DateDebut = employe.DateNouveauPoste;
        newPoste.IdPoste = employe.IdPoste;
        result.HistoriqueEmployes.Add(newPoste);
        result.Poste = null;
        result.Departement = null;

        return result;
    }
    public async Task<Employe> HandleDtosForInsert(EmployeDto employe)
    {
        Employe result = new Employe();
        
        result = await DtoToEmploye(employe);

        result.HistoriqueEmployes = new List<HistoriqueEmploye>();

        // Nouveau poste
        HistoriqueEmploye historique = new HistoriqueEmploye();
        historique.DateDebut = employe.DateNouveauPoste;
        historique.IdPoste = employe.IdPoste;

        result.HistoriqueEmployes.Add(historique);

        return result;
    }

    public async Task<Employe> HandleDtoForDelete(EmployeDto employe, PosteContext dbContext)
    {
        Employe result = new Employe();
        result = await DtoToEmploye(employe);
        result.HistoriqueEmployes = await dbContext.HistoriqueEmployes
            .Where(h => h.IdEmploye == result.IdEmploye)
            .ToListAsync();

        HistoriqueEmploye lastPoste = result.GetLastPoste();
        lastPoste.DateFin = employe.DateFinPoste;
        result.Statut = 5;
        result.Poste = null;
        result.Departement = null;

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
    [Column("statut")]
    public int Statut { get; set; }

    public ICollection<HistoriqueEmploye> HistoriqueEmployes { get; set; } = new List<HistoriqueEmploye>();

}
