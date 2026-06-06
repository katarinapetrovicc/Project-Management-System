using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.IO;
using System.Threading.Tasks;

namespace Projekat.Pages.EmployeePage
{
    public class DeleteModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DeleteModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        // Putanja do slike za prikaz
        public string EmployeeImagePath { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Employee = await _context.Employee.FirstOrDefaultAsync(e => e.ID == id);
            if (Employee == null) return NotFound();

            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            // 1️⃣ Ako postoji ime fajla u bazi i fajl postoji
            if (!string.IsNullOrEmpty(Employee.ProfileImage))
            {
                var dbFile = Path.Combine(rootPath, "images", "employee", Employee.ProfileImage);
                if (System.IO.File.Exists(dbFile))
                {
                    EmployeeImagePath = $"/images/employee/{Employee.ProfileImage}";
                    return Page();
                }
            }

            // 2️⃣ Ako nema ProfileImage → probaj FirstName.jpg / .png
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

            // 3️⃣ Ako ništa ne postoji → default
            EmployeeImagePath = "/images/employee/default.png";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employee.FindAsync(id);

            if (employee != null)
            {
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Employee");
        }
    }
}
