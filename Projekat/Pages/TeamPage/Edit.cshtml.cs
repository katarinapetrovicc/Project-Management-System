using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Projekat.Pages.TeamPage
{
    public class EditModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public EditModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Team Team { get; set; } = default!;

        [BindProperty]
        public IFormFile? LogoFile { get; set; }

        [BindProperty]
        public bool RemoveLogo { get; set; }

        // Putanja slike za prikaz
        public string TeamLogoPath { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Team = await _context.Team.FindAsync(id);
            if (Team == null) return NotFound();

            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            // 1️⃣ Ako postoji Logo u bazi
            if (!string.IsNullOrEmpty(Team.Logo))
            {
                var logoPath = Path.Combine(rootPath, "images", "team", Team.Logo);
                if (System.IO.File.Exists(logoPath))
                {
                    TeamLogoPath = $"/images/team/{Team.Logo}";
                    return Page();
                }
            }

            // 2️⃣ Ako nema Logo → probaj da generišeš ime fajla iz imena tima
            var normalizedName = Team.Name.Replace(" ", "").ToLower(); // "Marketing Team" → "marketingteam"
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var existingTeam = await _context.Team.FindAsync(Team.ID);
            if (existingTeam == null) return NotFound();

            existingTeam.Name = Team.Name;

            // Uklanjanje postojećeg logotipa
            if (RemoveLogo)
            {
                existingTeam.Logo = null;
            }

            // Upload novog logotipa
            if (LogoFile != null && LogoFile.Length > 0)
            {
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "team");
                if (!Directory.Exists(imagesPath))
                    Directory.CreateDirectory(imagesPath);

                var fileName = $"{Team.Name.Replace(" ", "")}{Path.GetExtension(LogoFile.FileName)}";
                var filePath = Path.Combine(imagesPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await LogoFile.CopyToAsync(stream);
                }

                existingTeam.Logo = fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("Team");
        }
    }
}
