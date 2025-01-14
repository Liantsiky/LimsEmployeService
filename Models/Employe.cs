using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsEmployeService.Models;
[Table("Employe")]
public class Employe
{
    [Key]
    [Column("id_employe")]
    public int IdEmploye { get; set; }
    [Column("matricule")]
    public string Matricule { get; set; }
    [Column("nom")]
    public string Nom { get; set; }
    [Column("prenom")]
    public string Prenom { get; set; }
    [Column("genre")]
    public int Genre { get; set; }
    [Column("CIN")]
    public string CIN { get; set; }
    [Column("contact")]
    public string Contact { get; set; }
    [Column("adresse")]
    public string Adresse { get; set; }
    [Column("manager")]
    public string Manager { get; set; }
    [Column("id_departement")]
    [ForeignKey("id_departement")]
    public int IdDepartement { get; set; }
    // public Departement? Departement { get; set; }
    [Column("id_poste")] 
    [ForeignKey("id_poste")]
    public int IdPoste { get; set; }
    // public Poste? Poste { get; set; }
}
