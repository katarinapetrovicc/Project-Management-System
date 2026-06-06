using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataBaseContext;
using DatabaseEntityLib;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Projekat.Pages.Activities
{
    public class CreateModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public CreateModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Activity Activity { get; set; } = default!;

        [BindProperty]
        public IFormFile? AttachmentFile { get; set; }

        public SelectList TaskList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            TaskList = new SelectList(await _context.Task.ToListAsync(), "ID", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (AttachmentFile != null)
            {
                using var ms = new MemoryStream();
                await AttachmentFile.CopyToAsync(ms);
                Activity.Attachment = ms.ToArray();
            }

            _context.Activity.Add(Activity);
            await _context.SaveChangesAsync();
            return RedirectToPage("Activity");
        }
    }
}
