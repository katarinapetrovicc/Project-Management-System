using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataBaseContext;
using DatabaseEntityLib;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Projekat.Pages.TeamMemberPage
{
    public class CreateModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public CreateModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public TeamMember TeamMember { get; set; } = default!;

        public SelectList TeamList { get; set; } = default!;
        public SelectList EmployeeList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var teams = await _context.Team.ToListAsync();
            var employees = await _context.Employee.ToListAsync();

            TeamList = new SelectList(teams, "ID", "Name");
            EmployeeList = new SelectList(employees, "ID", "FirstName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var teams = await _context.Team.ToListAsync();
                var employees = await _context.Employee.ToListAsync();
                TeamList = new SelectList(teams, "ID", "Name");
                EmployeeList = new SelectList(employees, "ID", "FirstName");
                return Page();
            }

            _context.TeamMember.Add(TeamMember);
            await _context.SaveChangesAsync();

            return RedirectToPage("./TeamMember");
        }
    }
}
