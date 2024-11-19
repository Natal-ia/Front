using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // Para HttpContext.Session

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

    // Propiedad para el rol del usuario
    public string? UserRole { get; set; }

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
                var response = await client.PostAsync("http://eva00:5050/login", content);

                if (response.IsSuccessStatusCode)
                {
                    // Leer la respuesta del servidor
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<LoginResponse>(responseBody);

                    var handler = new JwtSecurityTokenHandler();
                    if (responseData?.token != null)
                    {
                        // Guardar el JWT en la sesión
                        HttpContext.Session.SetString("JWT", responseData.token);

                        // Leer y decodificar el token JWT
                        var jsonToken = handler.ReadToken(responseData.token) as JwtSecurityToken;
                        UserRole = jsonToken?.Claims.First(claim => claim.Type == "role").Value;

                        // Verificar el rol del usuario
                        if (UserRole == "Administrador")
                        {
                            // Redirige al dashboard de administrador
                            return RedirectToPage("/sedes");
                        }
                        else if (UserRole == "Afiliado")
                        {
                            // Redirige al dashboard de afiliado
                            return RedirectToPage("/espacios");
                        }
                        else
                        {
                            // Si el rol no es reconocido, muestra un error
                            ModelState.AddModelError(string.Empty, "Acceso denegado. Rol no autorizado.");
                            return Page();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Token no válido.");
                        return Page();
                    }
                }
                else
                {
                    // Si la solicitud falla, muestra un mensaje de error
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

    // Clase para deserializar la respuesta del servidor
    public class LoginResponse
    {
        public string? token { get; set; }
    }
}
