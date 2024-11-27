
using FrameworkDigital_DesafioBackEnd.ORM.Context;

namespace FrameworkDigital_DesafioBackEnd.ORM.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public FrameworkDigitalDbContext _context;

        public BaseRepository(FrameworkDigitalDbContext context) {
            _context = context;

        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;

        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges(true);
        }
    }
}
