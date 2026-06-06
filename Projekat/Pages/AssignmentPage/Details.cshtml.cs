using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Threading.Tasks;

namespace Projekat.Pages.AssignmentPage
{
    public class DetailsModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DetailsModel(DB_Context_Class context)
        {
            _context = context;
        }

        public Assignment Assignment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Assignment = await _context.Assignment
                .Include(a => a.Employee)
                .Include(a => a.Activity)
                .FirstOrDefaultAsync(a => a.ID == id);

            if (Assignment == null) return NotFound();

            return Page();
        }
    }
}
