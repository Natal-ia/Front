using System.Collections.Generic; 
using System.Text.Json.Serialization;

namespace JaveFamilia.Models
{
public class Horario
{
    public string Id { get; set; }
    public bool Availavility { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
}
}