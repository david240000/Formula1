using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string? Url { get; set; }
    }
}
