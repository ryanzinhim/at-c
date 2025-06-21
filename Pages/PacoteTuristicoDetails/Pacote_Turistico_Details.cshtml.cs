using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EXPEXturism.Data;
using EXPEXturism.Model;

namespace EXPEXturism.Pages.PacoteTuristicoDetails
{
    public class Pacote_Turistico_DetailsModel : PageModel
    {
        private readonly EXPEXturismContexto _context;

        public Pacote_Turistico_DetailsModel(EXPEXturismContexto context)
        {
            _context = context;
        }

        [TempData]
        public string Mensagem { get; set; } = string.Empty;

        public Pacote_Turistico? Pacote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Mensagem = "ID do pacote turístico não encontrado.";
                return Page();
            }

            Pacote = await _context.Pacotes_Turisticos
                .Include(p => p.PacoteDestinos)
                .ThenInclude(pd => pd.Destino)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Pacote == null)
            {
                Mensagem = "Pacote turístico não encontrado.";
            }

            return Page();
        }
    }
}
