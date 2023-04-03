using Formula1.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formula1.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User? User { get; set; }
        [ForeignKey("Race")]
        public int RaceId { get; set; }
        public Race? Race { get; set; }
        [ForeignKey("Driver")]
        public int DriverId { get; set; }
        public Driver? Driver { get; set; }
        public RatingType? RatingValue { get; set; }
    }
}
