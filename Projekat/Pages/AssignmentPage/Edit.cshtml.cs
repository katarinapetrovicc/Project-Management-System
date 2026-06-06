using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataBaseContext;
using DatabaseEntityLib;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Projekat.Pages.AssignmentPage
{
    public class EditModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public EditModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Assignment Assignment { get; set; } = default!;

        public SelectList EmployeeList { get; set; } = default!;
        public SelectList ActivityList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Assignment = await _context.Assignment
                .Include(a => a.Employee)
                .Include(a => a.Activity)
                .FirstOrDefaultAsync(a => a.ID == id);

            if (Assignment == null) return NotFound();

            var employees = await _context.Employee.ToListAsync();
            var activities = await _context.Activity.ToListAsync();

            EmployeeList = new SelectList(employees, "ID", "FirstName", Assignment.EmployeeID);
            ActivityList = new SelectList(activities, "ID", "Name", Assignment.ActivityID);

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

            var existingAssignment = await _context.Assignment.FindAsync(Assignment.ID);
            if (existingAssignment == null) return NotFound();

            existingAssignment.EmployeeID = Assignment.EmployeeID;
            existingAssignment.ActivityID = Assignment.ActivityID;
            existingAssignment.AssignedDays = Assignment.AssignedDays;
            existingAssignment.Month = Assignment.Month;
            existingAssignment.Year = Assignment.Year;
            existingAssignment.Progress = Assignment.Progress;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Assigned");
        }
    }
}
