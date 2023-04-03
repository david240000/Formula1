using Formula1.Models;

namespace Formula1.Interfaces
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAllRatings();
        Task<IEnumerable<Rating>> GetRatingsByRaceAndUser(int raceId, string userId);
        Task<Rating> GetRatingById(int ratingId);
        Task<IEnumerable<Rating>> GetRatingsByUser(string userId);
        Task<IEnumerable<Rating>> GetRatingsByRace(int raceId);
        bool Add(IEnumerable<Rating> ratings);
        bool Add(Rating rating);
        bool Update(Rating rating);
        bool Delete(Rating rating);
        bool Save();

    }
}
