using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseEntityLib;
using Microsoft.EntityFrameworkCore;

namespace Projekat.Pages.WorkPackage
{
    public class CreateModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public CreateModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public DatabaseEntityLib.WorkPackage WorkPackage { get; set; } = new DatabaseEntityLib.WorkPackage();

        public SelectList ProjectList { get; set; } = null!;

        [BindProperty]
        public IFormFile? AttachmentFile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ProjectList = new SelectList(await _context.Project.ToListAsync(), "ID", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ProjectList = new SelectList(await _context.Project.ToListAsync(), "ID", "Name");
                return Page();
            }

            if (AttachmentFile != null && AttachmentFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await AttachmentFile.CopyToAsync(ms);
                WorkPackage.Attachment = ms.ToArray();
            }

            _context.WorkPackage.Add(WorkPackage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./WorkPackage");
        }
    }
}
