using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LimsEmployeService.Models;

[Table("Historique_Employe")]
public class HistoriqueEmploye
{
    [Key]
    [Column("id_historique_employe")]
    public int IdHistoriqueEmploye { get; set; }
    [Column("date_debut")]
    public DateTime DateDebut { get; set; }
    [Column("date_fin")]
    public DateTime? DateFin { get; set; }
    [Column("id_poste")]
    public int IdPoste { get; set; }
    [ForeignKey("IdPoste")]
    public Poste? Poste { get; set; }
    [Column("id_employe")]
    public int? IdEmploye { get; set; }
    [ForeignKey("IdEmploye")]
    [JsonIgnore]
    public Employe? Employe { get; set; }
}