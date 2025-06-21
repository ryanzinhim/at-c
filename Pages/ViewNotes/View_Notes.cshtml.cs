using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TurismoPorAi.Pages
{
    public class ViewNotesModel : PageModel
    {
        [BindProperty]
        public string NoteText { get; set; } = string.Empty;

        [BindProperty]
        public string FileName { get; set; } = string.Empty;

        public List<string> Files { get; set; } = new();

        public string? FileContent { get; set; }

        private readonly string _filesPath;

        public ViewNotesModel(IWebHostEnvironment env)
        {
            _filesPath = Path.Combine(env.WebRootPath, "files");
            Directory.CreateDirectory(_filesPath);
        }

        public void OnGet()
        {
            Files = Directory.GetFiles(_filesPath, "*.txt")
                             .Select(Path.GetFileName)
                             .ToList();
        }

        public IActionResult OnPostSave()
        {
            if (string.IsNullOrWhiteSpace(FileName) || string.IsNullOrWhiteSpace(NoteText))
            {
                ModelState.AddModelError("", "Nome do arquivo e conteúdo são obrigatórios.");
                return Page();
            }

            var safeFileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";
            var fullPath = Path.Combine(_filesPath, safeFileName);
            System.IO.File.WriteAllText(fullPath, NoteText);

            return RedirectToPage();
        }

        public void OnPostView(string fileToView)
        {
            var safeFileName = Path.GetFileName(fileToView);
            var fullPath = Path.Combine(_filesPath, safeFileName);

            if (System.IO.File.Exists(fullPath))
            {
                FileContent = System.IO.File.ReadAllText(fullPath);
            }

            Files = Directory.GetFiles(_filesPath, "*.txt")
                             .Select(Path.GetFileName)
                             .ToList();
        }
    }
}
