using Formula1.Data.Enums;
using Formula1.Models;

namespace Formula1.ViewModels
{
    public class RatingViewModel
    {
        public int DriverId { get; set; }
        public Driver? Driver { get; set; }
        public RatingType RatingValue { get; set; }
    }
}
