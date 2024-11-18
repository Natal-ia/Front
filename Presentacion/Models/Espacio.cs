using System.Collections.Generic; 
using System.Text.Json.Serialization;

namespace JaveFamilia.Models
{
        public class Espacio
        {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
        public int Capacity { get; set; }
        public decimal AffiliateRate { get; set; }
        public decimal NonAffiliateRate { get; set; }
        public decimal BeneficiaryRate { get; set; }
        public List<Horario> Horarios { get; set; }
    }
}
