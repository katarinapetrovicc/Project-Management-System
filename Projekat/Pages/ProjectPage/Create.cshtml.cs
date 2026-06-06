using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseEntityLib;

namespace Projekat.Pages.ProjectPage
{
    public class CreateModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public CreateModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async System.Threading.Tasks.Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Project.Add(Project);
            await _context.SaveChangesAsync();

            // Nakon kreiranja ide na listu projekata
            return RedirectToPage("./Project");
        }
    }
}


