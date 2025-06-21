using System.ComponentModel.DataAnnotations;
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
    public class CreateModel : PageModel
    {
        

        [BindProperty]
        [Required(ErrorMessage = "Selecione um cliente.")]
        public int ClienteId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Selecione um pacote.")]
        public int PacoteId { get; set; }

        public Reserva Reserva { get; set; } = default!;

        public List<Pacote_View_Model> Pacotes { get; set; } = new();
        public SelectList ClientesSelectList { get; set; } = default!;
        public SelectList PacotesSelectList { get; set; } = default!;

        private readonly EXPEXturismContexto _context;

        public CreateModel(EXPEXturismContexto context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Pacotes = _context.Pacotes_Turisticos
                .Select(p => new Pacote_View_Model
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    DataInicio = p.DataInicio.ToString("yyyy-MM-dd")
                }).ToList();

            ClientesSelectList = new SelectList(_context.Clientes, "ClienteId", "ClienteNome");
            PacotesSelectList = new SelectList(_context.Pacotes_Turisticos, "Id", "Nome");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Reserva = new Reserva
            {
                ClienteId = ClienteId,
                PacoteId = PacoteId,
                DataReserva = DateTime.Now
            };

            var cliente = await _context.Clientes.FindAsync(ClienteId);
            var pacote = await _context.Pacotes_Turisticos.FindAsync(PacoteId);

            if (cliente == null)
            {
                ModelState.AddModelError(string.Empty, $"Cliente inválido. ClienteId recebido: {ClienteId}");
                RepopulateLists();
                return Page();
            }

            if (pacote == null)
            {
                ModelState.AddModelError(string.Empty, $"Pacote inválido. Id recebido: {PacoteId}");
                RepopulateLists();
                return Page();
            }

            if (pacote.ReservasAtuais + 1 > pacote.CapacidadeMaxima)
            {
                ModelState.AddModelError(string.Empty, $"Capacidade máxima atingida para o pacote {pacote.Nome}.");
                RepopulateLists();
                return Page();
            }

            if (pacote.DataInicio < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, $"Evento do pacote {pacote.Nome} já ocorreu, reserva indisponível.");
                RepopulateLists();
                return Page();
            }

            var reservaExistente = await _context.Reservas
                .Include(r => r.PacoteTuristico)
                .AnyAsync(r => r.ClienteId == ClienteId && r.PacoteTuristico.DataInicio.Date == pacote.DataInicio.Date);

            if (reservaExistente)
            {
                ModelState.AddModelError(string.Empty, $"Você já possui uma reserva para o mesmo período ({pacote.DataInicio:dd/MM/yyyy}).");
                RepopulateLists();
                return Page();
            }

            Reserva.Cliente = cliente;
            Reserva.PacoteTuristico = pacote;
            Reserva.DataReserva = pacote.DataInicio;

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Errors.Select(e => e.ErrorMessage))}");
                }
                RepopulateLists();
                return Page();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Reserva.ExecutarReserva();
                _context.Reservas.Add(Reserva);
                pacote.ReservasAtuais++;
                _context.Pacotes_Turisticos.Update(pacote);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return RedirectToPage();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao processar a reserva. Tente novamente.");
                RepopulateLists();
                return Page();
            }
        }

        private void RepopulateLists()
        {
            Pacotes = _context.Pacotes_Turisticos
                .Select(p => new Pacote_View_Model
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    DataInicio = p.DataInicio.ToString("yyyy-MM-dd")
                }).ToList();

            ClientesSelectList = new SelectList(_context.Clientes, "ClienteId", "ClienteNome");
            PacotesSelectList = new SelectList(_context.Pacotes_Turisticos, "Id", "Nome");
        }
    }
}