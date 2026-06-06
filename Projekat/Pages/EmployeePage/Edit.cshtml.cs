using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.IO;
using System.Threading.Tasks;

namespace Projekat.Pages.EmployeePage
{
    public class EditModel : PageModel
    {
        private readonly DB_Context_Class _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(DB_Context_Class context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        [BindProperty]
        public IFormFile? ProfileImageFile { get; set; }

        [BindProperty]
        public bool RemoveProfileImage { get; set; }

        // Putanja za prikaz trenutne slike
        public string EmployeeImagePath { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Employee = await _context.Employee.FirstOrDefaultAsync(e => e.ID == id);
            if (Employee == null) return NotFound();

            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            // 1️⃣ Ako postoji ime u bazi i fajl postoji
            if (!string.IsNullOrEmpty(Employee.ProfileImage))
            {
                var dbFile = Path.Combine(rootPath, "images", "employee", Employee.ProfileImage);
                if (System.IO.File.Exists(dbFile))
                {
                    EmployeeImagePath = $"/images/employee/{Employee.ProfileImage}";
                    return Page();
                }
            }

            // 2️⃣ Proveri po imenu (FirstName.jpg/.png/.jpeg)
            var possibleFiles = new[]
            {
                $"{Employee.FirstName}.jpg",
                $"{Employee.FirstName}.png",
                $"{Employee.FirstName}.jpeg"
            };

            foreach (var file in possibleFiles)
            {
                var path = Path.Combine(rootPath, "images", "employee", file);
                if (System.IO.File.Exists(path))
                {
                    EmployeeImagePath = $"/images/employee/{file}";
                    return Page();
                }
            }

            // 3️⃣ Ako ništa nije nađeno → default
            EmployeeImagePath = "/images/employee/default.png";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var existingEmployee = await _context.Employee.FindAsync(Employee.ID);
            if (existingEmployee == null) return NotFound();

            // Ažuriranje osnovnih polja
            existingEmployee.FirstName = Employee.FirstName;
            existingEmployee.LastName = Employee.LastName;
            existingEmployee.Position = Employee.Position;
            existingEmployee.Email = Employee.Email;
            existingEmployee.Phone = Employee.Phone;

            // Brisanje stare slike
            if (RemoveProfileImage && !string.IsNullOrEmpty(existingEmployee.ProfileImage))
            {
                var oldImagePath = Path.Combine(_environment.WebRootPath, "images", "employee", existingEmployee.ProfileImage);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                existingEmployee.ProfileImage = null;
            }

            // Upload nove slike
            if (ProfileImageFile != null && ProfileImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "employee");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Path.GetFileName(ProfileImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImageFile.CopyToAsync(fileStream);
                }

                existingEmployee.ProfileImage = uniqueFileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("Employee");
        }
    }
}
