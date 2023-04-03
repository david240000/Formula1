using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formula1.Models
{
    public class Race
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int LapNumber { get; set; }
        [ForeignKey("Track")]
        public int TrackId { get; set; }
        public Track? Track { get; set; }
        public string? Year { get; set; }
        public string? Month { get; set; }
        public string? Day { get; set; }
    }
}
