using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Incentivos
{
    [Key]
    public int IncentivoId { get; set; }

    [Required(ErrorMessage = "Favor de introducir una fecha.")]
    public DateTime Fecha { get; set; }

    [ForeignKey("TecnicoId")]
    [Required(ErrorMessage = "Seleccionar un TécnicoId")]
    public int TecnicoId { get; set; }
    public Tecnicos Tecnico { get; set; }

    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Favor de introducir la descripción.")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Favor de introducir la cantidad de servicios.")]
    public int CantidadServicios { get; set; }

    [Required(ErrorMessage = "Favor de introducir el monto.")]
    public decimal Monto { get; set; }
}
