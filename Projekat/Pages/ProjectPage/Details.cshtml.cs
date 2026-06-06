using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseEntityLib;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using System.Threading.Tasks;

namespace Projekat.Pages.ProjectPage
{
    public class DetailsModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DetailsModel(DB_Context_Class context)
        {
            _context = context;
        }

        public Project Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Project = await _context.Project.FirstOrDefaultAsync(p => p.ID == id);

            if (Project == null) return NotFound();

            return Page();
        }
    }
}

