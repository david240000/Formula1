using Formula1.Data;
using Formula1.Interfaces;
using Formula1.Models;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DBContext _context;

        public RatingRepository(DBContext context)
        {
            _context = context;
        }
        public bool Add(Rating rating)
        {
            _context.Add(rating);
            return Save();
        }

        public bool Add(IEnumerable<Rating> ratings)
        {
            foreach(Rating rating in ratings)
            {
                _context.Add(rating);
            }
            return Save();
        }

        public bool Delete(Rating rating)
        {
            _context.Remove(rating);
            return Save();
        }

        public async Task<IEnumerable<Rating>> GetAllRatings()
        {
            return await _context.Ratings.Include(r=>r.User).Include(r => r.Race).ToListAsync();
        }

        public async Task<Rating> GetRatingById(int ratingId)
        {
            return await _context.Ratings.Include(r => r.User).Include(r => r.Race).FirstOrDefaultAsync(r=>r.Id == ratingId);
        }

        public async Task<IEnumerable<Rating>> GetRatingsByRace(int raceId)
        {
            return await _context.Ratings.Include(r => r.User).Include(r => r.Race).Where(r => r.RaceId == raceId).ToListAsync();
        }

        public async Task<IEnumerable<Rating>> GetRatingsByRaceAndUser(int raceId, string userId)
        {
            return await _context.Ratings.Include(r => r.User).Include(r => r.Race).Where(r => r.RaceId == raceId).Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Rating>> GetRatingsByUser(string userId)
        {
            return await _context.Ratings.Include(r => r.User).Include(r => r.Race).Include(r => r.Race.Track).Include(r => r.Driver).Where(r => r.UserId == userId).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Rating rating)
        {
            _context.Update(rating);
            return Save();
        }
    }
}
