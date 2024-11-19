using System.ComponentModel.DataAnnotations;

public class Sede
{
    public string? id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string? name { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    public string? address { get; set; }

    [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
    public string? phoneNumber { get; set; }

    [Required(ErrorMessage = "La ciudad es obligatoria.")]
    public string? city { get; set; }
}
