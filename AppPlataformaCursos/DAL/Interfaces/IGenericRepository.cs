namespace AppPlataformaCursos.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> PostEntity(T entity);

        Task<bool> UpdateEntity(T entity);

        Task<bool> DeleteEntity(int id);

        Task<IEnumerable<T>> GetEntities();

        Task<T> GetEntityById(int id);
    }
}
