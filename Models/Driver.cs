using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formula1.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Number { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team {get; set;}

        [ForeignKey("Nationality")]
        public int NationalityId { get; set; }
        public Nationality? Nationality { get; set; }
        public string? Image { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
    }
}
