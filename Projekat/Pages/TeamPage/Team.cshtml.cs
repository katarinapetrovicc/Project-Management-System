using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat.Pages.TeamPage
{
    public class TeamModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public TeamModel(DB_Context_Class context)
        {
            _context = context;
        }

        public IList<Team> Teams { get; set; } = new List<Team>();

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Teams = await _context.Team.ToListAsync();
            LoadTeamLogos();
        }

        private void LoadTeamLogos()
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "team");
            if (!Directory.Exists(imagesPath)) return;

            var images = Directory.GetFiles(imagesPath);

            foreach (var team in Teams)
            {
                var normalizedTeamName = team.Name.Replace(" ", "").ToLower();

                var file = images.FirstOrDefault(f =>
                    Path.GetFileNameWithoutExtension(f).Replace(" ", "").ToLower() == normalizedTeamName
                );

                team.Logo = file != null ? Path.GetFileName(file) : "default.png";
            }
        }
    }
}
