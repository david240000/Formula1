using Formula1.Data;
using Formula1.Interfaces;
using Formula1.Models;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DBContext _context;

        public DriverRepository(DBContext context)
        {
            _context = context;
        }
        public bool Add(Driver driver)
        {
            _context.Add(driver);
            return Save();
        }

        public bool Delete(Driver driver)
        {
            _context.Remove(driver);
            return Save();
        }

        public async Task<Driver> GetDriverById(int id)
        {
            return await _context.Drivers.Include(d => d.Nationality).Include(d => d.Team).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Driver>> GetDrivers()
        {
            return await _context.Drivers.Include(d => d.Nationality).Include(d => d.Team).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Driver driver)
        {
            _context.Update(driver);
            return Save();
        }
    }
}
