using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class SedeListModel : PageModel
{
    // Lista de sedes
    public List<Sede> Sedes { get; set; } = new List<Sede>();

    // Cargar las sedes al cargar la página
    public async Task OnGetAsync()
    {
        using (var client = new HttpClient())
        {
            try
            {
                var response = await client.GetAsync("http://eva00:5050/api/sedes");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Sedes = JsonSerializer.Deserialize<List<Sede>>(json);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al obtener las sedes: " + response.StatusCode);
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, "Error de red o problema con el servidor: " + ex);
            }
        }
    }

    // Eliminar una sede por ID
    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        using (var client = new HttpClient())
        {
            try
            {
                var response = await client.DeleteAsync($"http://eva00:5050/api/sedes/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // Redirige para recargar la lista de sedes después de la eliminación
                    return RedirectToPage();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al eliminar la sede: " + response.StatusCode);
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, "Error de red o problema con el servidor: " + ex);
            }
        }

        // Si ocurre un error, regresa la página con los errores
        return Page();
    }
}
