using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseEntityLib;
using DataBaseContext;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Projekat.Pages.EmployeePage
{
    public class CreateModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public CreateModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        [BindProperty]
        public IFormFile? ProfileImageFile { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Upload slike u wwwroot/images/employee folder
            if (ProfileImageFile != null)
            {
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "employee");
                if (!Directory.Exists(imagesPath))
                    Directory.CreateDirectory(imagesPath);

                var fileName = $"{Employee.FirstName}_{Employee.LastName}{Path.GetExtension(ProfileImageFile.FileName)}";
                var filePath = Path.Combine(imagesPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImageFile.CopyToAsync(stream);
                }

                Employee.ProfileImage = fileName; // Sačuvaj ime fajla u bazi
            }

            _context.Employee.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("Employee");
        }
    }
}
