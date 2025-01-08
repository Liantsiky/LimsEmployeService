using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeService.Models;

[Table("Poste")]
public class Poste
{   
    [Key]
    [Column("id_poste")]
    public int IdPoste { get; set; }
    public string Designation { get; set; }
}
