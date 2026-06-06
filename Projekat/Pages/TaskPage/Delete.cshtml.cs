using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DatabaseEntityLib;
using System.Threading.Tasks;

namespace Projekat.Pages.Tasks
{
    public class DeleteModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public DeleteModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public DatabaseEntityLib.Task? Task { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Task = await _context.Task
                .Include(t => t.WorkPackage)
                .FirstOrDefaultAsync(t => t.ID == id);

            if (Task == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var taskToDelete = await _context.Task.FindAsync(id);

            if (taskToDelete != null)
            {
                _context.Task.Remove(taskToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Task");
        }
    }
}
