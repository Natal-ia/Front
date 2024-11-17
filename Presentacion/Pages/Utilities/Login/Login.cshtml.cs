using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

public class LoginModel : PageModel
{
    // Propiedades para enlazar los datos del formulario
    [BindProperty]
    public string? Email { get; set; }

    [BindProperty]
    public string? Password { get; set; }

    // Método OnPostAsync para manejar el envío del formulario
    public async Task<IActionResult> OnPostAsync()
    {
        // Verificar que los datos del formulario no sean nulos o vacíos
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            ModelState.AddModelError(string.Empty, "Email and password are required.");
            return Page();
        }

        // Crear un objeto con los datos del login
        var loginData = new
        {
            email = Email,
            password = Password
        };

        // Serializar los datos a JSON
        var jsonData = JsonConvert.SerializeObject(loginData);

        using (var client = new HttpClient())
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST al servidor
            var response = await client.PostAsync("http://localhost:5050/login", content);

            if (response.IsSuccessStatusCode)
            {
                // Manejar la respuesta en caso de éxito
                var result = await response.Content.ReadAsStringAsync();
                // Redirigir a la página de destino en caso de login exitoso
                return RedirectToPage("/Dashboard"); // Cambia a tu página de destino
            }
            else
            {
                // Manejar errores
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }
    }
}
