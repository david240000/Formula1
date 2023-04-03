using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class Nationality
    {
        [Key]
        public int Id { get; set; }
        public string? Country { get; set; }
        public string? Flag { get; set; }
    }
}
