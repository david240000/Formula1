using Formula1.Interfaces;
using Formula1.Models;
using Formula1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Formula1.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDriverRepository _driverRepository;
        private readonly IRaceRepository _raceRepository;

        public DashboardController(IRatingRepository ratingRepository, IHttpContextAccessor httpContextAccessor, IDriverRepository driverRepository, IRaceRepository raceRepository)
        {
            _ratingRepository = ratingRepository;
            _httpContextAccessor = httpContextAccessor;
            _driverRepository = driverRepository;
            _raceRepository = raceRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var ratings = await _ratingRepository.GetRatingsByUser(userId);
            return View(ratings);
        }

        public async Task<IActionResult> Create()
        {

            IEnumerable<Race> races = await _raceRepository.GetRaces();
            var drivers = await _driverRepository.GetDrivers();
            RatingViewModel[] emptyRatings = new RatingViewModel[20];
            CreateRatingsViewModel ratings = new CreateRatingsViewModel()
            {
                Races = races,
                Drivers = drivers.ToList(),
                Ratings = emptyRatings
            };
            return View(ratings);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRatingsViewModel ratingVM)
        {

            if (ModelState.IsValid)
            {
                Race race = await _raceRepository.GetRaceById(ratingVM.RaceId);
                var userId = _httpContextAccessor.HttpContext.User.GetUserId();
                var ratings = await _ratingRepository.GetRatingsByRaceAndUser(ratingVM.RaceId, userId);
                if(ratings.Count() > 0)
                {
                    ModelState.AddModelError("", "Already has a ratings for this race");

                    return View(ratingVM);
                }
                List<Rating> newRatings = new List<Rating>();
                for(int i=0; i<20; i++)
                {
                    var rating = new Rating
                    {
                        RaceId = ratingVM.RaceId,
                        Race = race,
                        DriverId = ratingVM.Ratings[i].DriverId,
                        UserId = userId,
                        RatingValue = ratingVM.Ratings[i].RatingValue
                    };
                    newRatings.Add(rating);
                }

               _ratingRepository.Add(newRatings);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(ratingVM);
            }
        }
    }
}
