using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat.Pages.Activities
{
    public class EditModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public EditModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Activity Activity { get; set; } = default!;

        [BindProperty]
        public IFormFile? AttachmentFile { get; set; }

        [BindProperty]
        public bool RemoveAttachment { get; set; }

        public SelectList TaskList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Activity = await _context.Activity
                .Include(a => a.Task)
                .FirstOrDefaultAsync(a => a.ID == id);

            if (Activity == null) return NotFound();

            TaskList = new SelectList(
                await _context.Task.Select(t => new { t.ID, t.Name }).ToListAsync(),
                "ID", "Name", Activity.TaskID
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TaskList = new SelectList(
                    await _context.Task.Select(t => new { t.ID, t.Name }).ToListAsync(),
                    "ID", "Name", Activity.TaskID
                );
                return Page();
            }

            var existingActivity = await _context.Activity.FindAsync(Activity.ID);
            if (existingActivity == null) return NotFound();

            // Ažuriranje osnovnih polja
            existingActivity.TaskID = Activity.TaskID;
            existingActivity.Name = Activity.Name;
            existingActivity.Description = Activity.Description;
            existingActivity.PlannedHours = Activity.PlannedHours;
            existingActivity.ActualHours = Activity.ActualHours;
            existingActivity.DatePerformed = Activity.DatePerformed;

            // Uklanjanje starog fajla
            if (RemoveAttachment)
            {
                existingActivity.Attachment = null;
            }

            // Upload novog fajla
            if (AttachmentFile != null && AttachmentFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await AttachmentFile.CopyToAsync(ms);
                existingActivity.Attachment = ms.ToArray();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(Activity.ID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("Activity");
        }

        private bool ActivityExists(int id) =>
            _context.Activity.Any(a => a.ID == id);
    }
}
