using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using System.IO;
using System.Threading.Tasks;

namespace Projekat.Pages.ActivityPage
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
            var activity = await _context.Activity
                .FirstOrDefaultAsync(a => a.ID == id);

            if (activity == null || activity.Attachment == null || activity.Attachment.Length == 0)
            {
                return NotFound("Attachment not found.");
            }

            // Odredi ekstenziju fajla
            string extension = ".pdf"; // ili ".jpg", ".png" prema tipu fajla koji uploaduješ
            string fileName = $"attachment_{id}{extension}";

            string contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };

            return File(activity.Attachment, contentType, fileName);
        }
    }
}
