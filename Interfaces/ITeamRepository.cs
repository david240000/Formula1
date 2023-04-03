using Formula1.Models;

namespace Formula1.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<Team> GetTeamById(int id);
        Task<Team> GetTeamByIdNoTracking(int id);
        Task<Team> GetTeamByName(string name);
        bool Add(Team team);
        bool Update(Team team);
        bool Delete(Team team);
        bool Save();
    }
}
