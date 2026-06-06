using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Threading.Tasks;

namespace Projekat.Pages.Activities
{
    public class DeleteModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DeleteModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Activity Activity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Activity = await _context.Activity
                .Include(a => a.Task)
                .ThenInclude(t => t.WorkPackage)
                .FirstOrDefaultAsync(a => a.ID == id);

            if (Activity == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var activity = await _context.Activity.FindAsync(id);

            if (activity != null)
            {
                _context.Activity.Remove(activity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Activity");
        }
    }
}
