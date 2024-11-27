namespace FrameworkDigital_DesafioBackEnd.ORM.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Add(T entity);
        Task <T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
