using Formula1.Models;

namespace Formula1.ViewModels
{
    public class CreateTrackViewModel
    {
        public string Name { get; set; }
        public string? Lenght { get; set; }
        public string? Image { get; set; }
        public Nationality? Nationality { get; set; }
    }
}
