using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicosId { get; set; }

    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage ="Favor de introducir el nombre del técnico.")]
    public string Nombres { get; set; }

    [Required(ErrorMessage = "Favor de introducir el sueldo por hora del técnico.")]
    [Range(0,200000, ErrorMessage ="Favor de introducir un sueldo mayor que 1 y menor que 200000.")]
    public decimal SueldoHora { get; set; }
}
