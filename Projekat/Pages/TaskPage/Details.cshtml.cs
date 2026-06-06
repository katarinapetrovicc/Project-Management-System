using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DE = DatabaseEntityLib; // alias za DatabaseEntityLib

namespace Projekat.Pages.Tasks
{
    public class DetailsModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DetailsModel(DB_Context_Class context)
        {
            _context = context;
        }

        public DE.Task Task { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Task = await _context.Task
                .Include(t => t.WorkPackage) // učitavamo i povezani WorkPackage
                .FirstOrDefaultAsync(t => t.ID == id);

            if (Task == null) return NotFound();

            return Page();
        }
    }
}
