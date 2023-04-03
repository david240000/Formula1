namespace Formula1.ViewModels
{
    public class CreateTeamViewModel
    {
        public string Name { get; set; }
        public string? LogoImageUrl { get; set; }
        public List<string>? CarImageUrls { get; set; }
    }
}
