using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Threading.Tasks;

namespace Projekat.Pages.TeamMemberPage
{
    public class DetailsModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DetailsModel(DB_Context_Class context)
        {
            _context = context;
        }

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
    }
}
