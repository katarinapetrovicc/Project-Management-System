using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.IO;
using System.Threading.Tasks;

namespace Projekat.Pages.TeamPage
{
    public class DetailsModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DetailsModel(DB_Context_Class context)
        {
            _context = context;
        }

        public Team Team { get; set; } = default!;

        // Putanja slike za prikaz
        public string TeamLogoPath { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Team = await _context.Team.FirstOrDefaultAsync(t => t.ID == id);
            if (Team == null) return NotFound();

            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            // 1️⃣ Ako postoji Logo u bazi i fajl postoji
            if (!string.IsNullOrEmpty(Team.Logo))
            {
                var logoPath = Path.Combine(rootPath, "images", "team", Team.Logo);
                if (System.IO.File.Exists(logoPath))
                {
                    TeamLogoPath = $"/images/team/{Team.Logo}";
                    return Page();
                }
            }

            // 2️⃣ Ako nema Logo → probaj iz imena
            var normalizedName = Team.Name.Replace(" ", "").ToLower();
            var generatedFile = $"{normalizedName}.png";
            var generatedPath = Path.Combine(rootPath, "images", "team", generatedFile);

            if (System.IO.File.Exists(generatedPath))
            {
                TeamLogoPath = $"/images/team/{generatedFile}";
            }
            else
            {
                // 3️⃣ Ako ništa → default
                TeamLogoPath = "/images/team/default.png";
            }

            return Page();
        }
    }
}
