/*using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using JaveFamilia.Models;

namespace JaveFamilia.Pages
{
    public class VerReservasModel : PageModel
    {
        public List<Reserva> Reservas { get; private set; }

        public async Task OnGetAsync(string usuarioID)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"http://localhost:5050/api/reserva/{usuarioID}");
                Reservas = JsonSerializer.Deserialize<List<Reserva>>(response);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using JaveFamilia.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JaveFamilia.Pages
{
    public class VerReservasModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public VerReservasModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ReservaApi");
        }

        public List<Reserva> Reservas { get; set; } = new();

        public async Task OnGetAsync()
        {
            var usuarioID = "user123"; // Simulación
            var response = await _httpClient.GetAsync($"http://localhost:5021/api/Reserva/{usuarioID}");


            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Reservas = JsonSerializer.Deserialize<List<Reserva>>(json);
            }
        }
    }
}

*/


using Microsoft.AspNetCore.Mvc.RazorPages;
using JaveFamilia.Controllers;
using JaveFamilia.Models;
using System.Collections.Generic;
using System.Linq;

namespace JaveFamilia.Pages
{
    public class VerReservasModel : PageModel
    {
        public List<Reserva> Reservas { get; private set; } = new();

        public void OnGet()
        {
            var usuarioID = "user123"; // Simulación
            Reservas = ReservaController.Reservas
                        .Where(r => r.UsuarioID == usuarioID)
                        .ToList();
        }
    }
}
