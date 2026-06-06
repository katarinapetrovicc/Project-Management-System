using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

using SystemTask = System.Threading.Tasks.Task;

namespace Projekat.Pages.ProjectPage
{
    public class ProjectModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public ProjectModel(DB_Context_Class context)
        {
            _context = context;
        }

        // Lista projekata
        public IList<Project> Projects { get; set; } = default!;

        public async SystemTask OnGetAsync()
        {
            if (_context.Project != null)
            {
                Projects = await _context.Project.ToListAsync();
            }
        }
    }
}
