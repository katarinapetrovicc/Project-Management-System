using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataBaseContext;
using DatabaseEntityLib;

namespace Projekat.Pages.TaskPage
{
    public class CreateModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public CreateModel(DB_Context_Class context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            WorkPackageList = new SelectList(
                _context.WorkPackage.OrderBy(wp => wp.Name),
                "ID",
                "Name"
            );
            return Page();
        }

        [BindProperty]
        public DatabaseEntityLib.Task Task { get; set; } = default!;

        public SelectList WorkPackageList { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Task == null || Task == null)
            {
                WorkPackageList = new SelectList(
                    _context.WorkPackage.OrderBy(wp => wp.Name),
                    "ID",
                    "Name"
                );
                return Page();
            }

            _context.Task.Add(Task);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Task");
        }
    }
}

