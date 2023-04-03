using Formula1.Data;
using Formula1.Interfaces;

namespace Formula1.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly DBContext _context;

        public ImageRepository(DBContext context)
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            var image = _context.Image.FirstOrDefault(x => x.Id == id);
            _context.Remove(image);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
