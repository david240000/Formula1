using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formula1.Models
{
    public class Track
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Lenght { get; set; }
        public string? Image { get; set; }
        [ForeignKey("Nationality")]
        public int NationalityId { get; set; }
        public Nationality? Nationality { get; set; }
    }
}
