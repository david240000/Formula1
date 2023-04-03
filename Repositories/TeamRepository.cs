using Formula1.Data;
using Formula1.Interfaces;
using Formula1.Models;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DBContext _context;
        private readonly IImageRepository _imageRepository;

        public TeamRepository(DBContext context, IImageRepository imageRepository)
        {
            _context = context;
            _imageRepository = imageRepository;
        }
        public bool Add(Team team)
        {
            _context.Teams.Add(team);
            return Save();
        }

        public bool Delete(Team team)
        {
            foreach(var image in team.CarImageUrls)
            {
                _imageRepository.Delete(image.Id);
            }
            _context.Remove(team);
            return Save();
        }

        public async Task<Team> GetTeamById(int id)
        {
            return await _context.Teams.Include(i => i.CarImageUrls).FirstOrDefaultAsync(t=>t.Id ==id);
        }

        public async Task<Team> GetTeamByIdNoTracking(int id)
        {
            return await _context.Teams.Include(i => i.CarImageUrls).AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Team> GetTeamByName(string name)
        {
            return await _context.Teams.Where(t => t.Name.Contains(name)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Team team)
        {
            _context.Update(team);
            return Save();
        }
    }
}
