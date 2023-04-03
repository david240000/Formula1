using Formula1.Models;

namespace Formula1.Interfaces
{
    public interface ITrackRepository
    {
        Task<IEnumerable<Track>> GetTracks();
        Task<Track> GetTrackById(int id);
        Task<Track> GetTrackByIdNoTracking(int id);
        bool Add(Track track);
        bool Update(Track track);
        bool Delete(Track track);
        bool Save();
    }
}
