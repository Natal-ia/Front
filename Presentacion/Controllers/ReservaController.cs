/*using Microsoft.AspNetCore.Mvc;
using JaveFamilia.Models;

namespace JaveFamilia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private static List<Reserva> reservas = new List<Reserva>();

       [HttpPut]
public IActionResult Put([FromBody] Reserva nuevaReserva)
{
    if (string.IsNullOrEmpty(nuevaReserva.UsuarioID) || 
        string.IsNullOrEmpty(nuevaReserva.EspacioID) || 
        string.IsNullOrEmpty(nuevaReserva.HorarioID))
    {
        return BadRequest(new { message = "Datos incompletos para la reserva." });
    }

    nuevaReserva.EstadoPago = EstadoPago.Exitoso; // Asumiendo que se procesa correctamente

    // Simulación de auto-incremento usando base de datos
    reservas.Add(nuevaReserva);

    return Ok(nuevaReserva);
}


        [HttpGet("{usuarioID}")]
        public IActionResult GetByUsuario(string usuarioID)
        {
            var userReservas = reservas.Where(r => r.UsuarioID == usuarioID).ToList();
            return Ok(userReservas);
        }
    }
}*/
using Microsoft.AspNetCore.Mvc;
using JaveFamilia.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace JaveFamilia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        public static List<Reserva> Reservas = new List<Reserva>
        {
            new Reserva
            {
                Id = 1,
                UsuarioID = "user123",
                EspacioID = "Espacio A",
                HorarioID = "10:00 AM - 11:00 AM",
                FechaAgendamiento = DateTime.Now,
                FechaReserva = DateTime.Now.AddDays(1),
                EstadoPago = EstadoPago.Exitoso
            },
            new Reserva
            {
                Id = 2,
                UsuarioID = "user123",
                EspacioID = "Espacio B",
                HorarioID = "2:00 PM - 3:00 PM",
                FechaAgendamiento = DateTime.Now,
                FechaReserva = DateTime.Now.AddDays(2),
                EstadoPago = EstadoPago.Pendiente
            },
            new Reserva
            {
                Id = 3,
                UsuarioID = "user456",
                EspacioID = "Espacio C",
                HorarioID = "4:00 PM - 5:00 PM",
                FechaAgendamiento = DateTime.Now,
                FechaReserva = DateTime.Now.AddDays(3),
                EstadoPago = EstadoPago.Rechazado
            }
        };

        public static List<Reserva> GetReservas()
        {
            return Reservas;
        }

        [HttpGet("{usuarioID}")]
        public IActionResult GetByUsuario(string usuarioID)
        {
            var userReservas = Reservas.Where(r => r.UsuarioID == usuarioID).ToList();
            return Ok(userReservas);
        }
    }
}
