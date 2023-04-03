using Formula1.Models;

namespace Formula1.ViewModels
{
    public class CreateDriverViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Number { get; set; }
        public int TeamId { get; set; }
        public Team? Team { get; set; }
        public int NationalityId { get; set; }
        public Nationality? Nationality { get; set; }
        public string? Image { get; set; }
        public IEnumerable<Team>? Teams { get; set; }
    }
}
