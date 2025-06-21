using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EXPEXturism.Data;
using EXPEXturism.Model;

namespace EXPEXturism.Pages.CRUDReserva
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly EXPEXturism.Data.EXPEXturismContexto _context;

        public EditModel(EXPEXturism.Data.EXPEXturismContexto context)
        {
            _context = context;
        }

        [BindProperty]
        public Reserva Reserva { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Where(r => !r.isDeleted)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (reserva == null)
            {
                return NotFound();
            }
            Reserva = reserva;
           ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteNome");
           ViewData["PacoteId"] = new SelectList(_context.Pacotes_Turisticos, "Id", "Nome");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Reserva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(Reserva.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
