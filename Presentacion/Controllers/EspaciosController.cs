using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using JaveFamilia.Models;

namespace JaveFamilia.Controllers
{
    public class EspaciosController
    {
        private readonly HttpClient _httpClient;

        public EspaciosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Espacio>> GetEspaciosAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5050/api/espacio");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Espacio>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
