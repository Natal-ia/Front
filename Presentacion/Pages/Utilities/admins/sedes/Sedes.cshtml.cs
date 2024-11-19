using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SedeModel : PageModel
{
    // Propiedades para el modelo de datos
    [BindProperty]
    [Required(ErrorMessage = "El ID es obligatorio.")]
    public string? Id { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string? Name { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "La dirección es obligatoria.")]
    public string? Address { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
    public string? PhoneNumber { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "La ciudad es obligatoria.")]
    public string? City { get; set; }

    // Método para manejar la solicitud POST
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Si la validación falla, regresa la página con los errores
            return Page();
        }

        // Crear el objeto de datos para la solicitud
        var data = new
        {
            id = Id,
            name = Name,
            address = Address,
            phoneNumber = PhoneNumber,
            city = City
        };

        // Convertir el objeto a JSON
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Hacer la solicitud HTTP POST al servidor
        using (var client = new HttpClient())
        {
            try
            {
                var response = await client.PostAsync("http://eva00:5050/api/sedes", content);

                if (response.IsSuccessStatusCode)
                {
                    // Si la solicitud es exitosa, redirige a la página de confirmación
                    return RedirectToPage("/Success");
                }
                else
                {
                    // Si la solicitud falla, muestra un mensaje de error
                    ModelState.AddModelError(string.Empty, "Error al registrar la sede: " + response);
                    return Page();
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores de red o problemas con el servidor
                ModelState.AddModelError(string.Empty, "Error de red o problema con el servidor: " + ex);
                return Page();
            }
        }
    }
}
