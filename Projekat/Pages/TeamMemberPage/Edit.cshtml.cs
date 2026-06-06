using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Threading.Tasks;
using System.Linq;

namespace Projekat.Pages.TeamMemberPage
{
    public class EditModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public EditModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public TeamMember TeamMember { get; set; } = default!;

        public SelectList TeamList { get; set; } = default!;
        public SelectList EmployeeList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            TeamMember = await _context.TeamMember.FindAsync(id);

            if (TeamMember == null) return NotFound();

            var teams = await _context.Team.ToListAsync();
            var employees = await _context.Employee.ToListAsync();

            TeamList = new SelectList(teams, "ID", "Name", TeamMember.TeamID);
            EmployeeList = new SelectList(employees, "ID", "FirstName", TeamMember.EmployeeID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var teams = await _context.Team.ToListAsync();
                var employees = await _context.Employee.ToListAsync();
                TeamList = new SelectList(teams, "ID", "Name", TeamMember.TeamID);
                EmployeeList = new SelectList(employees, "ID", "FirstName", TeamMember.EmployeeID);
                return Page();
            }

            var existingTeamMember = await _context.TeamMember.FindAsync(TeamMember.ID);
            if (existingTeamMember == null) return NotFound();

            existingTeamMember.TeamID = TeamMember.TeamID;
            existingTeamMember.EmployeeID = TeamMember.EmployeeID;
            existingTeamMember.RoleInTeam = TeamMember.RoleInTeam;

            await _context.SaveChangesAsync();

            return RedirectToPage("./TeamMember");
        }
    }
}
