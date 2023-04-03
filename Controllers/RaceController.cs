using Formula1.Interfaces;
using Formula1.Models;
using Formula1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Formula1.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RaceController(IRaceRepository raceRepository, ITrackRepository trackRepository, IHttpContextAccessor httpContextAccessor)
        {
            _raceRepository = raceRepository;
            _trackRepository = trackRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetRaceBySeason(DateTime.Now.Year.ToString());
            return View(races);
        }
        public async Task<IActionResult> Create()
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            IEnumerable<Track> tracks = await _trackRepository.GetTracks();
            CreateRaceViewModel race = new CreateRaceViewModel()
            {
                Tracks = tracks
            };
            return View(race);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            if (ModelState.IsValid)
            {
                Track track = await _trackRepository.GetTrackById(raceVM.TrackId);
                var race = new Race
                {
                    Name = raceVM.Name,
                    LapNumber = raceVM.LapNumber,
                    TrackId = raceVM.TrackId,
                    Track = track,
                    Year = raceVM.Year,
                    Month = raceVM.Month,
                    Day = raceVM.Day
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(raceVM);
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _raceRepository.GetRaceById(id);
            return View(race);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            var race = await _raceRepository.GetRaceById(id);
            var tracks = await _trackRepository.GetTracks();
            if (race == null) return View("Error");

            var teamVM = new CreateRaceViewModel
            {
                Name = race.Name,
                LapNumber = race.LapNumber,
                TrackId=race.TrackId,
                Year = race.Year,
                Month= race.Month,
                Day = race.Day,
                Tracks = tracks
            };

            return View(teamVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateRaceViewModel raceVM)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            if (ModelState.IsValid)
            {
                Track track = await _trackRepository.GetTrackById(raceVM.TrackId);
                var race = new Race
                {
                    Id = id,
                    Name = raceVM.Name,
                    LapNumber = raceVM.LapNumber,
                    TrackId = raceVM.TrackId,
                    Track = track,
                    Year = raceVM.Year,
                    Month = raceVM.Month,
                    Day = raceVM.Day
                };
                _raceRepository.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(raceVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            var race = await _raceRepository.GetRaceById(id);
            if (race == null) return View("Error");

            _raceRepository.Delete(race);
            return RedirectToAction("Index");
        }
    }
}
