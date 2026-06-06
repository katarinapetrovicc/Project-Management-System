using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projekat.Pages.TeamMemberPage
{
    public class TeamMemberModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public TeamMemberModel(DB_Context_Class context)
        {
            _context = context;
        }

        public IList<TeamMember> TeamMember { get; set; } = default!;

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            TeamMember = await _context.TeamMember
                .Include(tm => tm.Team)
                .Include(tm => tm.Employee)
                .ToListAsync();
        }
    }
}
