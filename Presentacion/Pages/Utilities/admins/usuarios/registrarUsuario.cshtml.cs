using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class RegistrarUsuarioModel : PageModel
{
    // Propiedades para el modelo de datos
    [BindProperty]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string? FirstName { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string? LastName { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
    public string? Email { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    public DateTime Birthday { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
    public string? PhoneNumber { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
    public string? TipoDocumento { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "El número de documento es obligatorio.")]
    public string? Document { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    public string? Password { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria.")]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
    public string? ConfirmPassword { get; set; }

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
            email = Email,
            password = Password,
            firstName = FirstName,
            lastName = LastName,
            birthday = Birthday.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            phoneNumber = PhoneNumber,
            tipoDocumento = TipoDocumento,
            document = Document
        };

        // Convertir el objeto a JSON
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Hacer la solicitud HTTP POST al servidor
        using (var client = new HttpClient())
        {
            try
            {
                var response = await client.PostAsync("http://eva00:5050/register", content);

                if (response.IsSuccessStatusCode)
                {
                    // Si la solicitud es exitosa, redirige a la página de bienvenida
                    ModelState.AddModelError(string.Empty, "Usuario registrado correctamente: " + response);
                    return Page();

                }
                else
                {
                    // Si la solicitud falla, muestra un mensaje de error
                    ModelState.AddModelError(string.Empty, "Error en el registro: " + response);
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
