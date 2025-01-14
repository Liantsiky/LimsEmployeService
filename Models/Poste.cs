using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsEmployeService.Models;

[Table("Poste")]
public class Poste
{   
    [Key]
    [Column("id_poste")]
    public int IdPoste { get; set; }
    [Column("designation")]
    public string Designation { get; set; }
}
