using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EXPEXturism.Data;
using EXPEXturism.Model;
using System.Linq;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly EXPEXturismContexto _context;

    public IndexModel(ILogger<IndexModel> logger, EXPEXturismContexto context)
    {
        _logger = logger;
        _context = context;
    }

    public List<Pacote_Turistico> PacotesDisponiveis { get; set; } = new();

    public void OnGet()
    {
        PacotesDisponiveis = _context.Pacotes_Turisticos
        .Include(p => p.PacoteDestinos)
        .ThenInclude(pd => pd.Destino)
        .OrderBy(d => d.Nome)
        .ToList();
    }
}
