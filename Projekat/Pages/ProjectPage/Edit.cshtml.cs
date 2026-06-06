using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseEntityLib;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat.Pages.ProjectPage
{
    public class EditModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public EditModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Project = await _context.Project.FirstOrDefaultAsync(p => p.ID == id);

            if (Project == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Project.Any(p => p.ID == Project.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Redirect na listu projekata (Index.cshtml u istom folderu)
            return RedirectToPage("./Project");
        }
    }
}
