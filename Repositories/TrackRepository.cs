using Formula1.Data;
using Formula1.Interfaces;
using Formula1.Models;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly DBContext _context;

        public TrackRepository(DBContext  context)
        {
            _context = context;
        }
        public bool Add(Track track)
        {
            _context.Add(track);
            return Save();
        }

        public bool Delete(Track track)
        {
            _context.Remove(track);
            return Save();
        }

        public async Task<Track> GetTrackById(int id)
        {
            return await _context.Tracks.Include(t => t.Nationality).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Track> GetTrackByIdNoTracking(int id)
        {
            return await _context.Tracks.Include(t => t.Nationality).AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Track>> GetTracks()
        {
            return await _context.Tracks.Include(t => t.Nationality).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0?true:false;
        }

        public bool Update(Track track)
        {
            _context.Update(track);
            return Save();
        }
    }
}
