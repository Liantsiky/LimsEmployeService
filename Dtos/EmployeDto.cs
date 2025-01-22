using System.Text.Json.Serialization;
using LimsEmployeService.Models;

namespace LimsEmployeService.Dtos;

public class EmployeDto
{
    [JsonPropertyName("idEmploye")]
    public int IdEmploye { get; set; }
    [JsonPropertyName("matricule")]
    public string? Matricule { get; set; }
    [JsonPropertyName("nom")]
    public string? Nom { get; set; }
    [JsonPropertyName("prenom")]
    public string? Prenom { get; set; }
    [JsonPropertyName("genre")]
    public int Genre { get; set; }
    [JsonPropertyName("cin")]
    public string? Cin { get; set; }
    [JsonPropertyName("contact")]
    public string? Contact { get; set; }
    [JsonPropertyName("adresse")]
    public string? Adresse { get; set; }
    [JsonPropertyName("manager")]
    public string? Manager { get; set; }
    [JsonPropertyName("idDepartement")]
    public int? IdDepartement { get; set; }
    [JsonPropertyName("departement")]
    public Departement? Departement { get; set; }
    [JsonPropertyName("idPoste")] 
    public int IdPoste { get; set; }
    [JsonPropertyName("poste")]
    public Poste? Poste { get; set; }
    [JsonPropertyName("dateNouveauPoste")]
    public DateTime DateNouveauPoste { get; set; }
    [JsonPropertyName("dateFinAncienPoste")]
    public DateTime? DateFinPoste { get; set; }
    [JsonPropertyName("historiqueEmployes")]
    public ICollection<HistoriqueEmployeDto> HistoriqueEmployes = new List<HistoriqueEmployeDto>();
}