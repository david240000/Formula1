using Formula1.Models;

namespace Formula1.ViewModels
{
    public class CreateRaceViewModel
    {
        public string? Name { get; set; }
        public int LapNumber { get; set; }
        public int TrackId { get; set; }
        public string? Year { get; set; }
        public string? Month { get; set; }
        public string? Day { get; set; }

        public IEnumerable<Track>? Tracks { get; set; }
    }
}
