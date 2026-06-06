using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseEntityLib;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using System.Threading.Tasks;

// Alias da izbegnemo konflikt namespace/klasa
using WP = DatabaseEntityLib.WorkPackage;

namespace Projekat.Pages.WorkPackagePage
{
    public class DetailsModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DetailsModel(DB_Context_Class context)
        {
            _context = context;
        }

        public WP WorkPackage { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            WorkPackage = await _context.WorkPackage
                .Include(w => w.Project) // Učitavamo i povezani projekat
                .FirstOrDefaultAsync(w => w.ID == id);

            if (WorkPackage == null) return NotFound();

            return Page();
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
