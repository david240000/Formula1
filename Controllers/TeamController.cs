using Formula1.Interfaces;
using Formula1.Models;
using Formula1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Formula1.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TeamController(ITeamRepository teamRepository, IImageRepository imageRepository, IHttpContextAccessor httpContextAccessor)
        {
            _teamRepository = teamRepository;
            _imageRepository = imageRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Team> teams = await _teamRepository.GetTeams();
            return View(teams);
        }

        public IActionResult Create()
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamViewModel teamVM)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            if (ModelState.IsValid)
            {
                List<Image> images = new List<Image>();
                foreach(var image in teamVM.CarImageUrls)
                {
                    Image im = new Image()
                    {
                        Url = image
                    };
                    images.Add(im);        
                }
                var team = new Team
                {
                    Name = teamVM.Name,
                    LogoImageUrl = teamVM.LogoImageUrl, 
                    CarImageUrls = images
                };
                _teamRepository.Add(team);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(teamVM);

        }

        public async Task<IActionResult> Detail(int id)
        {
            Team team = await _teamRepository.GetTeamById(id);
            return View(team);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            var team = await _teamRepository.GetTeamById(id);
            if (team == null) return View("Error");

            List<string> images = new List<string>();
            foreach(var item in team.CarImageUrls)
            {
                images.Add(item.Url);
            }

            var teamVM = new CreateTeamViewModel
            {
                Name = team.Name,
                LogoImageUrl = team.LogoImageUrl,
                CarImageUrls = images
            };

            teamVM.CarImageUrls.Add("");
            return View(teamVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateTeamViewModel teamVM)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit");
                return View("Edit", teamVM);
            }

            var teamOld = await _teamRepository.GetTeamByIdNoTracking(id);
            if(teamOld == null) return View("Error");

            List<Image> images = new List<Image>();
            foreach (var image in teamVM.CarImageUrls)
            {
                Image im = new Image()
                {
                    Url = image
                };
                images.Add(im);
            }

            foreach(var image in teamOld.CarImageUrls)
            {
                _imageRepository.Delete(image.Id);
            }

            var team = new Team
            {
                Id = id,
                Name = teamVM.Name,
                LogoImageUrl = teamVM.LogoImageUrl,
                CarImageUrls = images
            };

            _teamRepository.Update(team);
            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            var team = await _teamRepository.GetTeamById(id);
            if (team == null) return View("Error");

            _teamRepository.Delete(team);
            return RedirectToAction("Index");
        }


        //[HttpDelete, ActionName("Delete")]
        //public async Task<IActionResult> DeleteClub(int id)
        //{

        //    var team = await _teamRepository.GetTeamById(id);
        //    if (team == null) return View("Error");
            
        //    _teamRepository.Delete(team);
        //    return RedirectToAction("Index");
        //}
    }
}
