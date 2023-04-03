using Formula1.Models;

namespace Formula1.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        bool Add(User user);
        bool Update(User user);
        Task Delete(User user);
        bool Save();
    }
}