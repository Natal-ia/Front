using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JaveFamilia.Models;
using System.IdentityModel.Tokens.Jwt;

namespace JaveFamilia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private static List<Reserva> reservas = new List<Reserva>();

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] Reserva nuevaReserva)

        {
            // Obtiene el ID del usuario desde el token JWT
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader)) return Unauthorized(new { message = "No token provided." });

            var token = authHeader.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var userId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;

            if (userId == null) return Unauthorized(new { message = "Invalid token." });

            // Valida los datos de la reserva
            if (string.IsNullOrEmpty(nuevaReserva.EspacioID) || string.IsNullOrEmpty(nuevaReserva.HorarioID))
            {
                return BadRequest(new { message = "Datos incompletos para la reserva." });
            }

            nuevaReserva.UsuarioID = userId;
            nuevaReserva.EstadoPago = EstadoPago.Exitoso; // Simulación de éxito
            nuevaReserva.Id = reservas.Count + 1; // Simulación de ID autoincremental

            reservas.Add(nuevaReserva);
            return Ok(nuevaReserva);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            // Obtiene el ID del usuario desde el token JWT
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader)) return Unauthorized(new { message = "No token provided." });

            var token = authHeader.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var userId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;

            if (userId == null) return Unauthorized(new { message = "Invalid token." });

            // Filtra las reservas del usuario
            var userReservations = reservas.Where(r => r.UsuarioID == userId).ToList();
            return Ok(userReservations);
        }

        }

}
