using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EXPEXturism.Data;
using EXPEXturism.Model;

namespace EXPEXturism.Pages.CRUDReserva
{
    public class IndexModel : PageModel
    {
        private readonly EXPEXturism.Data.EXPEXturismContexto _context;

        public IndexModel(EXPEXturism.Data.EXPEXturismContexto context)
        {
            _context = context;
        }

        public IList<Reserva> Reserva { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.PacoteTuristico)
                .Where(r => !r.isDeleted)
                .ToListAsync();
        }
    }
}
