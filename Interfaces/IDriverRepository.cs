using Formula1.Models;

namespace Formula1.Interfaces
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetDrivers();
        Task<Driver> GetDriverById(int id);
        bool Add(Driver driver);
        bool Update(Driver driver);
        bool Delete(Driver driver);
        bool Save();
    }
}
