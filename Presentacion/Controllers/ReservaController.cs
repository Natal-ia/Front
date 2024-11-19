using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using JaveFamilia.Models;

namespace JaveFamilia.Controllers
{
    public class ReservaController
    {
        private readonly HttpClient _httpClient;

        public ReservaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Reserva>> GetReservasAsync(string usuarioId)
        {
            // Llamada al endpoint de la API
            var response = await _httpClient.GetAsync("http://eva00:5050/api/gestionReservas.gestionReservas/reservas/reservas");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var reservas = JsonSerializer.Deserialize<List<Reserva>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Filtrar por usuarioId
            return reservas.FindAll(r => r.UsuarioID == usuarioId);
        }
    }
}
