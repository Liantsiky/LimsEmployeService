using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using LimsEmployeService.Dtos;

namespace LimsEmployeService.Models;
[Table("Employe")]
public class Employe
{
    public async Task<Employe> HandleDtosForInsert(EmployeDto employe)
    {
        Employe result = new Employe();
        string dtoAsJson = JsonSerializer.Serialize(employe);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        result = JsonSerializer.Deserialize<Employe>(dtoAsJson, options);
        result.HistoriqueEmployes = new List<HistoriqueEmploye>();
        Console.WriteLine(dtoAsJson);
        Console.WriteLine(JsonSerializer.Serialize(result));

        // Nouveau poste
        HistoriqueEmploye historique = new HistoriqueEmploye();
        historique.DateDebut = employe.DateNouveauPoste;
        historique.IdPoste = employe.IdPoste;

        result.HistoriqueEmployes.Add(historique);

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

    public ICollection<HistoriqueEmploye> HistoriqueEmployes { get; set; }

}
