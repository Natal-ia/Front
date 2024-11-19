using Microsoft.AspNetCore.Mvc;
using JaveFamilia.Models;

namespace JaveFamilia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
/*        private static List<Reserva> reservas = new List<Reserva>();

        [HttpPut]
        [HttpPut]
        public IActionResult Put([FromBody] Reserva nuevaReserva)
        {
            var usuarioId = HttpContext.Session.GetString("UserID"); // Obtener el ID del usuario de la sesión

            if (string.IsNullOrEmpty(usuarioId) ||
                string.IsNullOrEmpty(nuevaReserva.EspacioID) ||
                string.IsNullOrEmpty(nuevaReserva.HorarioID))
            {
                return BadRequest(new { message = "Datos incompletos para la reserva." });
            }

            nuevaReserva.UsuarioID = usuarioId; // Establecer el ID del usuario en la reserva
            nuevaReserva.EstadoPago = EstadoPago.Exitoso;

            reservas.Add(nuevaReserva);

            return Ok(nuevaReserva);
        }



        [HttpGet("{usuarioID}")]
        public IActionResult GetByUsuario(string usuarioID)
        {
            var userReservas = reservas.Where(r => r.UsuarioID == usuarioID).ToList();
            return Ok(userReservas);
        }*/
    }
}