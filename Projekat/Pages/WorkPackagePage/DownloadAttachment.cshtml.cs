using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using System.IO;
using System.Threading.Tasks;

namespace Projekat.Pages.WorkPackagePage
{
    public class DownloadAttachmentModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DownloadAttachmentModel(DB_Context_Class context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var workPackage = await _context.WorkPackage
                .FirstOrDefaultAsync(wp => wp.ID == id);

            if (workPackage == null || workPackage.Attachment == null || workPackage.Attachment.Length == 0)
                return NotFound("Attachment not found.");

            // Pretpostavljamo da ime fajla postoji u WorkPackage, možeš dodati polje AttachmentFileName u bazu
            string fileName = string.IsNullOrEmpty(workPackage.Name)
                ? $"attachment_{id}"
                : workPackage.Name;

            // Odredi ekstenziju iz imena fajla ili postavi default
            string extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension)) extension = ".pdf"; // default ekstenzija
            fileName += extension;

            // Odredi MIME tip
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

            return File(workPackage.Attachment, contentType, fileName);
        }
    }
}
