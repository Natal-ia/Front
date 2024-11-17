using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class SignupModel : PageModel
{
    public string? Email { get; set; }
    public string? Password { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        // Crea un objeto con los datos del login
        var loginData = new
        {
            email = Email,
            password = Password
        };

        // Serializa los datos a JSON
        var jsonData = JsonConvert.SerializeObject(loginData);

        using (var client = new HttpClient())
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Realiza la solicitud POST
            var response = await client.PostAsync("http://localhost:5050/login", content);

            if (response.IsSuccessStatusCode)
            {
                // Manejar la respuesta en caso de éxito
                var result = await response.Content.ReadAsStringAsync();
                // Realiza alguna acción, como redirigir a otra página
                return RedirectToPage("/Success"); // Página de éxito
            }
            else
            {
                // Manejar errores
                ModelState.AddModelError(string.Empty, "Error en el login.");
                return Page();
            }
        }
    }
}
