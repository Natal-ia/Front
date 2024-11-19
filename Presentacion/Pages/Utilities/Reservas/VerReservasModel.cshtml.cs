using Microsoft.AspNetCore.Mvc.RazorPages;
using JaveFamilia.Controllers;
using JaveFamilia.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaveFamilia.Pages
{
    public class VerReservasModel : PageModel
    {
        private readonly ReservaController _reservaController;

        public VerReservasModel(ReservaController reservaController)
        {
            _reservaController = reservaController;
        }

        public List<Reserva> Reservas { get; private set; } = new();

        public async Task OnGetAsync()

        {
            ViewData["UserRole"] = HttpContext.Session.GetString("UserRole");
            var usuarioId = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

            if (!string.IsNullOrEmpty(usuarioId))
            {
                Reservas = await _reservaController.GetReservasAsync(usuarioId);
            }
        }
    }
}
