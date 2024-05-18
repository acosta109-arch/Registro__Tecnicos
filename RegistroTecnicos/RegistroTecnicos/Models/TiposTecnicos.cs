using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    public int TipoId { get; set; }

    [Required(ErrorMessage = "Favor de introducir una descripción.")]
    public string Descripcion { get; set; }
}
