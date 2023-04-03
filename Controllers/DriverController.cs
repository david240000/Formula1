using Formula1.Interfaces;
using Formula1.Models;
using Formula1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Formula1.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDriverRepository _driverRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DriverController(IDriverRepository driverRepository, ITeamRepository teamRepository, IHttpContextAccessor httpContextAccessor)
        {
            _driverRepository = driverRepository;
            _teamRepository = teamRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var drivers = await _driverRepository.GetDrivers();
            return View(drivers);
        }

        public async Task<IActionResult> Create()
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            IEnumerable<Team> teams = await _teamRepository.GetTeams();
            CreateDriverViewModel driver = new CreateDriverViewModel()
            {
                Teams = teams
            };
            return View(driver);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDriverViewModel driverVM)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            if (ModelState.IsValid)
            {
                Team team = await _teamRepository.GetTeamById(driverVM.TeamId);
                var driver = new Driver
                {
                    FirstName = driverVM.FirstName,
                    LastName = driverVM.LastName,
                    Number = driverVM.Number,
                    Image = driverVM.Image,
                    TeamId = driverVM.TeamId,
                    Team = team,
                    Nationality =new Nationality
                    {
                        Country = driverVM.Nationality.Country,
                        Flag = driverVM.Nationality.Flag
                    }
                };
                _driverRepository.Add(driver);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(driverVM);
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            Driver driver = await _driverRepository.GetDriverById(id);
            return View(driver);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            var driver = await _driverRepository.GetDriverById(id);
            var teams = await _teamRepository.GetTeams();
            if (driver == null) return View("Error");

            var driverVM = new CreateDriverViewModel
            {
                FirstName = driver.FirstName,
                LastName= driver.LastName,
                Image = driver.Image,
                Number = driver.Number,
                Nationality = driver.Nationality,
                NationalityId = driver.NationalityId,
                TeamId = driver.TeamId,
                Team = driver.Team,
                Teams = teams
            };

            return View(driverVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateDriverViewModel driverVM)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            if (ModelState.IsValid)
            {
                Team team = await _teamRepository.GetTeamById(driverVM.TeamId);
                var driver = new Driver
                {
                    Id = id,
                    FirstName = driverVM.FirstName,
                    LastName = driverVM.LastName,
                    Image = driverVM.Image,
                    Number = driverVM.Number,
                    Nationality = driverVM.Nationality,
                    NationalityId = driverVM.NationalityId,
                    TeamId = driverVM.TeamId,
                    Team = team,
                };
                _driverRepository.Update(driver);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(driverVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var permission = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!permission)
            {
                return RedirectToAction("Login", "user");
            }
            var driver = await _driverRepository.GetDriverById(id);
            if (driver == null) return View("Error");

            _driverRepository.Delete(driver);
            return RedirectToAction("Index");
        }
    }
}
