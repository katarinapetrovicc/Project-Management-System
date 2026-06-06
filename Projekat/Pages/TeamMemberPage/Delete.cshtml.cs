using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Threading.Tasks;

namespace Projekat.Pages.TeamMemberPage
{
    public class DeleteModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DeleteModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public TeamMember TeamMember { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            TeamMember = await _context.TeamMember
                .Include(tm => tm.Team)
                .Include(tm => tm.Employee)
                .FirstOrDefaultAsync(tm => tm.ID == id);

            if (TeamMember == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var teamMember = await _context.TeamMember.FindAsync(id);

            if (teamMember != null)
            {
                _context.TeamMember.Remove(teamMember);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./TeamMember");
        }
    }
}
