using System;

namespace JaveFamilia.Models
{
    public enum EstadoPago
    {
        Pendiente,
        Exitoso,
        Rechazado
    }

    public class Reserva
    {
        public int Id { get; set; }
        public string UsuarioID { get; set; }
        public string EspacioID { get; set; }
        public string HorarioID { get; set; }
        public DateTime FechaAgendamiento { get; set; }
        public DateTime FechaReserva { get; set; }
        public EstadoPago EstadoPago { get; set; }
    }
}
