using Formula1.Models;
using Formula1.ViewModels;

namespace Formula1.ViewModels
{
    public class CreateRatingsViewModel
    {
        public RatingViewModel[]? Ratings { get; set; }
        public int RaceId { get; set; }
        public IEnumerable<Race>? Races { get; set; }
        public List<Driver>? Drivers { get; set; }
    }
}
