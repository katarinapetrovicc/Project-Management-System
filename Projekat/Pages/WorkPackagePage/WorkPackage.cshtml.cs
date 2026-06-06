using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.IO;
using Task = System.Threading.Tasks.Task;

namespace Projekat.Pages.WorkPackagePage
{
    public class WorkPackageModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public WorkPackageModel(DB_Context_Class context)
        {
            _context = context;
        }

        // Lista WorkPackage-ova (inicijalizovana da nikad ne bude null)
        public IList<DatabaseEntityLib.WorkPackage> WorkPackages { get; set; } = new List<DatabaseEntityLib.WorkPackage>();

        // GET: učitaj sve WorkPackage-ove sa povezanim Project-om
        public async Task OnGetAsync()
        {
            if (_context.WorkPackage != null)
            {
                WorkPackages = await _context.WorkPackage
                    .Include(wp => wp.Project)
                    .ToListAsync();
            }
        }

        // Preuzimanje attachment-a
        public async Task<FileResult?> OnGetDownloadAsync(int id)
        {
            var wp = await _context.WorkPackage.FindAsync(id);
            if (wp == null || wp.Attachment == null || wp.Attachment.Length == 0)
                return null;

            // Odredi tip fajla po ekstenziji originalnog imena ako postoji
            string extension = ".pdf"; // podrazumevana ekstenzija
            string contentType = "application/octet-stream";

            // Ako je ime fajla poznato, možemo izvući ekstenziju
            // (ovde možeš promeniti logiku ako čuvaš originalno ime fajla)
            if (wp.Name != null)
            {
                extension = Path.GetExtension(wp.Name).ToLower();
            }

            contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream"
            };

            string fileName = $"{wp.Name}_Attachment{extension}";

            return File(wp.Attachment, contentType, fileName);
        }
    }
}
