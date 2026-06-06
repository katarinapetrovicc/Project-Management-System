using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DatabaseEntityLib;

namespace Projekat.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public EditModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public DatabaseEntityLib.Task Task { get; set; } = default!;

        public SelectList WorkPackageList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Task == null)
            {
                return NotFound();
            }

            var taskEntity = await _context.Task.FirstOrDefaultAsync(t => t.ID == id);

            if (taskEntity == null)
            {
                return NotFound();
            }

            Task = taskEntity;

            WorkPackageList = new SelectList(_context.WorkPackage.OrderBy(wp => wp.Name), "ID", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                WorkPackageList = new SelectList(_context.WorkPackage.OrderBy(wp => wp.Name), "ID", "Name");
                return Page();
            }

            _context.Attach(Task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(Task.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Task");
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.ID == id);
        }
    }
}
