using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseEntityLib;
using DataBaseContext;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Projekat.Pages.TeamPage
{
    public class CreateModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public CreateModel(DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Team Team { get; set; } = new Team();

        [BindProperty]
        public IFormFile? LogoFile { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Upload loga u wwwroot/images/team folder
            if (LogoFile != null)
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

                Team.Logo = fileName; // Sačuvaj ime fajla u bazi
            }
            else
            {
                // Ako nije postavljen logo, pokušaj da ga pronađe po imenu tima u folderu
                Team.Logo = GetLogoByTeamName(Team.Name);
            }

            _context.Team.Add(Team);
            await _context.SaveChangesAsync();

            return RedirectToPage("Team");
        }

        private string GetLogoByTeamName(string teamName)
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "team");
            if (!Directory.Exists(imagesPath)) return "default.png";

            var files = Directory.GetFiles(imagesPath);

            var normalizedName = teamName.Replace(" ", "").ToLower();

            var file = files.FirstOrDefault(f =>
                Path.GetFileNameWithoutExtension(f).Replace(" ", "").ToLower() == normalizedName);

            return file != null ? Path.GetFileName(file) : "default.png";
        }
    }
}
