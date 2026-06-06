using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DatabaseEntityLib;
using System.IO;
using System.Threading.Tasks;

namespace Projekat.Pages.WorkPackagePage
{
    public class EditModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public EditModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public DatabaseEntityLib.WorkPackage? WorkPackage { get; set; }

        [BindProperty]
        public IFormFile? AttachmentFile { get; set; }

        [BindProperty]
        public bool RemoveAttachment { get; set; } = false;

        public SelectList ProjectList { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            WorkPackage = await _context.WorkPackage.FindAsync(id);

            if (WorkPackage == null) return NotFound();

            ProjectList = new SelectList(await _context.Project.ToListAsync(), "ID", "Name", WorkPackage.ProjectID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var workPackageToUpdate = await _context.WorkPackage.FindAsync(id);
            if (workPackageToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync(workPackageToUpdate, "WorkPackage",
                wp => wp.Name, wp => wp.Description, wp => wp.PlannedDays, wp => wp.Priority, wp => wp.ProjectID))
            {
                if (RemoveAttachment)
                {
                    workPackageToUpdate.Attachment = null;
                }

                if (AttachmentFile != null && AttachmentFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await AttachmentFile.CopyToAsync(ms);
                    workPackageToUpdate.Attachment = ms.ToArray();
                }

                await _context.SaveChangesAsync();
                return RedirectToPage("./WorkPackage");
            }

            ProjectList = new SelectList(await _context.Project.ToListAsync(), "ID", "Name", workPackageToUpdate.ProjectID);
            return Page();
        }
        public async Task<FileResult?> OnGetDownloadAsync(int id)
        {
            var wp = await _context.WorkPackage.FindAsync(id);
            if (wp == null || wp.Attachment == null || wp.Attachment.Length == 0)
                return null;

            // Odredi ekstenziju fajla ili MIME tip
            string extension = ".pdf"; // primer, može se prilagoditi
            string contentType = extension switch
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
