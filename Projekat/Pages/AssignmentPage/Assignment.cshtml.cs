using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projekat.Pages.AssignmentPage
{
    public class AssignedModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public AssignedModel(DB_Context_Class context)
        {
            _context = context;
        }

        public IList<Assignment> AssignmentList { get; set; } = default!;

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            AssignmentList = await _context.Assignment
                .Include(a => a.Employee)
                .Include(a => a.Activity)
                .ToListAsync();
        }
    }
}
