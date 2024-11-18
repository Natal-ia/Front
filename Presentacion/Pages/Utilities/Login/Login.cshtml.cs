using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class LoginModel : PageModel
{
    // Propiedades para el modelo de datos
    [BindProperty]
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
    public string? Email { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string? Password { get; set; }

    // Método Login para manejar la solicitud POST
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Si la validación falla, regresa la página con los errores
            return Page();
        }

        // Crear el objeto de datos para la solicitud
        var data = new { email = Email, password = Password };

        // Convertir el objeto a JSON
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Hacer la solicitud HTTP POST al servidor
        using (var client = new HttpClient())
        {
            try
            {
                Console.WriteLine(content.ReadAsStringAsync().Result);

                var response = await client.PostAsync("http://eva00:5050/login", content);

                if (response.IsSuccessStatusCode)
                {
                    // Si la solicitud es exitosa, redirige al dashboard
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                    return RedirectToPage("/espacios"); // Asegúrate de que esta ruta sea la correcta
                }
                else
                {
                    // Si la solicitud falla, muestra un mensaje de error
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                    ModelState.AddModelError(string.Empty, "Error en el login: " + response.ReasonPhrase);
                    return Page();
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores de red o problemas con el servidor
                ModelState.AddModelError(string.Empty, "Error de red o problema con el servidor: " + ex.Message);
                return Page();
            }
        }
    }
}
