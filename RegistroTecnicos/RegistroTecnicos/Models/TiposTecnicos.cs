using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    public int TipoId { get; set; }

    [Required(ErrorMessage = "Favor de introducir una descripción.")]
    public string Descripcion { get; set; }


    [ForeignKey("IncentivoId")]
    public int? IncentivoId { get; set; }

    public Incentivos Incentivo { get; set; }

    [NotMapped]
    public decimal MontoIncentivos { get; set; }
}
