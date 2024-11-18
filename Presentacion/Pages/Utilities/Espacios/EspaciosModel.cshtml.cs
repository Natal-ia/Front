using System.Collections.Generic;
using System.Threading.Tasks;
using JaveFamilia.Controllers;
using JaveFamilia.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JaveFamilia.Pages
{
    public class EspaciosModel : PageModel
    {
        private readonly EspaciosController _espaciosController;

        public List<Espacio> Espacios { get; set; } = new();

        public EspaciosModel(EspaciosController espaciosController)
        {
            _espaciosController = espaciosController;
        }

        public async Task OnGetAsync()
        {
            Espacios = await _espaciosController.GetEspaciosAsync();
        }
    }
}
