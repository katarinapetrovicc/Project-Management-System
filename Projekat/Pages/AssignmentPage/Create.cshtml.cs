using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataBaseContext;
using DatabaseEntityLib;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Projekat.Pages.AssignmentPage
{
    public class CreateModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public CreateModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Assignment Assignment { get; set; } = default!;

        public SelectList EmployeeList { get; set; } = default!;
        public SelectList ActivityList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var employees = await _context.Employee.ToListAsync();
            var activities = await _context.Activity.ToListAsync();

            EmployeeList = new SelectList(employees, "ID", "FirstName");
            ActivityList = new SelectList(activities, "ID", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var employees = await _context.Employee.ToListAsync();
                var activities = await _context.Activity.ToListAsync();
                EmployeeList = new SelectList(employees, "ID", "FirstName", Assignment.EmployeeID);
                ActivityList = new SelectList(activities, "ID", "Name", Assignment.ActivityID);
                return Page();
            }

            _context.Assignment.Add(Assignment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Assigned");
        }
    }
}
