using Formula1.Models;

namespace Formula1.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetRaces();
        Task<IEnumerable<Race>> GetRaceBySeason(string year);
        Task<Race> GetRaceById(int id);
        bool Add(Race race);
        bool Update(Race race);
        bool Delete(Race race);
        bool Save();
    }
}
