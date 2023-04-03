using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LogoImageUrl { get; set; }
        public ICollection<Image>? CarImageUrls { get; set; }
    }
}
