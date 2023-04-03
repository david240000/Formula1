using Formula1.Interfaces;
using Formula1.Models;
using Formula1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Formula1.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TrackController(ITrackRepository trackRepository, IHttpContextAccessor httpContextAccessor)
        {
            _trackRepository = trackRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Track> teams = await _trackRepository.GetTracks();
            return View(teams);
        }

        public IActionResult Create()
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (permission)
            {
                return View();
            }

            return RedirectToAction("Login", "user");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrackViewModel trackVM)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            if (ModelState.IsValid)
            {
                var track = new Track
                {
                    Name = trackVM.Name,
                    Image = trackVM.Image,
                    Lenght = trackVM.Lenght,
                    Nationality = new Nationality()
                    {
                        Country = trackVM.Nationality.Country,
                        Flag = trackVM.Nationality.Flag
                    }
                };
                _trackRepository.Add(track);
                return RedirectToAction("Index");
            }

            return View(trackVM);

        }

        public async Task<IActionResult> Detail(int id)
        {
            Track track = await _trackRepository.GetTrackById(id);
            return View(track);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            var track = await _trackRepository.GetTrackById(id);
            if (track == null) return View("Error");

            var teamVM = new EditTrackViewModel
            {
                Id = track.Id,
                Name = track.Name,
                Image = track.Image,
                Lenght=track.Lenght,
                NationalityId = track.NationalityId,
                Nationality = track.Nationality
            };

            return View(teamVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTrackViewModel trackVM)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit");
                return View("Edit", trackVM);
            }


            
            var track = new Track
            {
                Id = id,
                Name = trackVM.Name,
                Image = trackVM.Image,
                Lenght = trackVM.Lenght,
                NationalityId = trackVM.NationalityId,
                Nationality = trackVM.Nationality
            };

            _trackRepository.Update(track);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            var track = await _trackRepository.GetTrackById(id);
            if (track == null) return View("Error");

            _trackRepository.Delete(track);
            return RedirectToAction("Index");
        }
    }
}
