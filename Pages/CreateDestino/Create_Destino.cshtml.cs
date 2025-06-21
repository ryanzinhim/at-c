using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EXPEXturism.Model;
using EXPEXturism.Data;

namespace EXPEXturism.Pages.CreateDestino;

public class CreateDestinoModel : PageModel
{

    private readonly EXPEXturismContexto _context;

    public CreateDestinoModel(EXPEXturismContexto context)
    {
        _context = context;
    }

    [BindProperty]
    public required Destino Destino { get; set; }

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


        _context.Destinos.Add(Destino);
        _context.SaveChanges();

        Mensagem = $"Destino '{Destino.Nome}' cadastrado com sucesso!";

        return RedirectToPage();
    }
}
