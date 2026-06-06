using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Projekat.Pages.WorkPackages
{
    public class DeleteModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public DeleteModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public DatabaseEntityLib.WorkPackage? WorkPackage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            WorkPackage = await _context.WorkPackage
                .Include(wp => wp.Project)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (WorkPackage == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var wp = await _context.WorkPackage.FindAsync(id);

            if (wp != null)
            {
                _context.WorkPackage.Remove(wp);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./WorkPackage");
        }
        public async Task<FileResult?> OnGetDownloadAsync(int id)
        {
            var wp = await _context.WorkPackage.FindAsync(id);
            if (wp == null || wp.Attachment == null || wp.Attachment.Length == 0)
                return null;

            string fileName = string.IsNullOrEmpty(wp.Name) ? $"attachment_{id}" : wp.Name;
            string extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension)) extension = ".pdf";
            fileName += extension;

            string contentType = extension.ToLower() switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".txt" => "text/plain",
                _ => "application/octet-stream"
            };

            return File(wp.Attachment, contentType, fileName);
        }

    }
}
