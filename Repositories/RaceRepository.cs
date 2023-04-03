using Formula1.Data;
using Formula1.Interfaces;
using Formula1.Models;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Repositories
{
    public class RaceRepository : IRaceRepository
    {
        private readonly DBContext _context;

        public RaceRepository(DBContext context)
        {
            _context = context;
        }
        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<Race> GetRaceById(int id)
        {
            return await _context.Races.Include(r=>r.Track.Nationality).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Race>> GetRaceBySeason(string year)
        {
            return await _context.Races.Include(r => r.Track).Where(r => r.Year.Contains(year)).ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetRaces()
        {
            return await _context.Races.ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
