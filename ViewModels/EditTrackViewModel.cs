using Formula1.Models;

namespace Formula1.ViewModels
{
    public class EditTrackViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Lenght { get; set; }
        public string? Image { get; set; }
        public int NationalityId { get; set; }
        public Nationality? Nationality { get; set; }
    }
}
