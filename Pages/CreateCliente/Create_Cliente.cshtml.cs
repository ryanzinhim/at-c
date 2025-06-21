using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EXPEXturism.Data;
using EXPEXturism.Model;

namespace EXPEXturism.Pages.CreateCliente
{
    public class Create_ClienteModel : PageModel
    {

        private readonly EXPEXturismContexto _context;

        public Create_ClienteModel(EXPEXturismContexto context)
        {
            _context = context;
        }

        [BindProperty]
        public required Cliente Cliente { get; set; }


        [TempData]
        public string Mensagem { get; set; } = string.Empty;


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            _context.Clientes.Add(Cliente);
            _context.SaveChanges();

            Mensagem = $"Cliente '{Cliente.ClienteNome}' cadastrado com sucesso!";

            return RedirectToPage();
        }
    }
}

