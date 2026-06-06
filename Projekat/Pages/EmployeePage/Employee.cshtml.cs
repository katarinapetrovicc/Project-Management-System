using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat.Pages.EmployeePage
{
    public class EmployeeModel : PageModel
    {
        private readonly DB_Context_Class _context;

        [BindProperty]
        public string SearchText { get; set; }

        static bool reverseFirstName = false;
        static bool reverseLastName = false;

        public EmployeeModel(DB_Context_Class context)
        {
            _context = context;
        }

        public IList<Employee> Employees { get; set; } = new List<Employee>();

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Employees = await _context.Employee.ToListAsync();
            LoadEmployeeImages();
        }

        public async System.Threading.Tasks.Task OnGetSortByFirstName()
        {
            Employees = reverseFirstName
                ? await _context.Employee.OrderBy(e => e.FirstName).ToListAsync()
                : await _context.Employee.OrderByDescending(e => e.FirstName).ToListAsync();
            reverseFirstName = !reverseFirstName;
            LoadEmployeeImages();
        }

        public async System.Threading.Tasks.Task OnGetSortByLastName()
        {
            Employees = reverseLastName
                ? await _context.Employee.OrderBy(e => e.LastName).ToListAsync()
                : await _context.Employee.OrderByDescending(e => e.LastName).ToListAsync();
            reverseLastName = !reverseLastName;
            LoadEmployeeImages();
        }

        public async System.Threading.Tasks.Task OnPost()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Employees = await _context.Employee.ToListAsync();
            }
            else
            {
                Employees = await _context.Employee
                    .Where(e => EF.Functions.Like(e.FirstName, $"%{SearchText}%") ||
                                EF.Functions.Like(e.LastName, $"%{SearchText}%"))
                    .ToListAsync();
            }

            LoadEmployeeImages();
        }

        private void LoadEmployeeImages()
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "employee");
            if (!Directory.Exists(imagesPath)) return;

            var images = Directory.GetFiles(imagesPath);

            foreach (var emp in Employees)
            {
                var file = images.FirstOrDefault(f =>
                    Path.GetFileNameWithoutExtension(f).ToLower() == emp.FirstName.ToLower());

                emp.ProfileImage = file != null ? Path.GetFileName(file) : "default.png";
            }
        }
    }
}
