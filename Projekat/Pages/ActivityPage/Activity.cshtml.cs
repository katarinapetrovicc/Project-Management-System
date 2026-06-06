using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseContext;
using DatabaseEntityLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Projekat.Pages.ActivityPage
{
    public class ActivityModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public ActivityModel(DB_Context_Class context)
        {
            _context = context;
        }

        // Lista aktivnosti (inicijalizovana da nikad ne bude null)
        public IList<Activity> Activities { get; set; } = new List<Activity>();

        // GET: učitaj sve aktivnosti sa povezanim Task-om i WorkPackage-om
        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Activities = await _context.Activity
                .Include(a => a.Task)
                    .ThenInclude(t => t.WorkPackage)
                .ToListAsync();
        }

        // Preuzimanje attachment-a
        public async Task<FileResult?> OnGetDownloadAsync(int id)
        {
            var activity = await _context.Activity.FindAsync(id);
            if (activity == null || activity.Attachment == null || activity.Attachment.Length == 0)
            {
                return null;
            }

            // Odredi tip fajla po ekstenziji (hardkodirano ili prema tipu koji uploaduješ)
            string extension = ".pdf"; // primer, ako su svi fajlovi PDF
            string contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream"
            };

            string fileName = $"{activity.Name}_Attachment{extension}";

            return File(activity.Attachment, contentType, fileName);
        }

    }
}
