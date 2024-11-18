using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using JaveFamilia.Models;

namespace JaveFamilia.Pages
{
    public class VerReservasModel : PageModel
    {
        public List<Reserva> Reservas { get; private set; } = new();

        public async Task OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                // Agrega el token JWT al encabezado
                var token = Request.Cookies["jwt_token"]; // O usa localStorage si está configurado
                if (string.IsNullOrEmpty(token)) return;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetStringAsync("http://localhost:5050/api/reservas");
                Reservas = JsonSerializer.Deserialize<List<Reserva>>(response);
            }
        }
    }
}
