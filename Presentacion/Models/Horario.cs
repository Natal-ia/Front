using System.Collections.Generic; 
using System.Text.Json.Serialization;

namespace JaveFamilia.Models
{
    public class Horario
    {
        public string Id { get; set; }
        [JsonPropertyName("availavility")] // Solo si el JSON original usa esta ortografía
        public bool Availability { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}