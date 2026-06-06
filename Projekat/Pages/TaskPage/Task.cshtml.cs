using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseEntityLib;

namespace Projekat.Pages.TaskPage
{
    public class TaskModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public TaskModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        public IList<DatabaseEntityLib.Task> Tasks { get; set; } = default!;

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            if (_context.Task != null)
            {
                Tasks = await _context.Task
                    .Include(t => t.WorkPackage)
                    .ToListAsync();
            }
            else
            {
                Tasks = new List<DatabaseEntityLib.Task>();
            }
        }

    }
}
