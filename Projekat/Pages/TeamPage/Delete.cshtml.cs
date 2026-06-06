using System.IO;
using System.Threading.Tasks;
using DatabaseEntityLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;

namespace Projekat.Pages.TeamPage
{
    public class DeleteModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DeleteModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Team Team { get; set; }

        public string TeamLogoPath { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Team = await _context.Team.FirstOrDefaultAsync(t => t.ID == id);

            if (Team == null)
            {
                return NotFound();
            }

            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            // 1️⃣ Ako u bazi postoji Logo fajl → koristi njega
            if (!string.IsNullOrEmpty(Team.Logo))
            {
                var logoPath = Path.Combine(rootPath, "images", "team", Team.Logo);
                if (System.IO.File.Exists(logoPath))
                {
                    TeamLogoPath = $"/images/team/{Team.Logo}";
                    return Page();
                }
            }

            // 2️⃣ Ako nema Logo u bazi → probaj da generišeš ime fajla iz Team.Name
            var normalizedName = Team.Name.Replace(" ", "").ToLower(); // Marketing Team -> marketingteam
            var generatedFile = $"{normalizedName}.png";
            var generatedPath = Path.Combine(rootPath, "images", "team", generatedFile);

            if (System.IO.File.Exists(generatedPath))
            {
                TeamLogoPath = $"/images/team/{generatedFile}";
            }
            else
            {
                // 3️⃣ Ako ništa od toga → default
                TeamLogoPath = "/images/team/default.png";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var team = await _context.Team.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            _context.Team.Remove(team);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Team");
        }
    }
}
