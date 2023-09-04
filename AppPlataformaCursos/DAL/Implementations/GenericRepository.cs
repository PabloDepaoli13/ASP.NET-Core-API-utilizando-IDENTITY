using AppPlataformaCursos.DAL.DataContext;
using AppPlataformaCursos.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppPlataformaAprendizaje.DAL.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AplicationDbContext _context;

        public GenericRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteEntity(int id)
        {
            var result = false;
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return result;
            }
            _context.Set<T>().Remove(entity);
            result = await _context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<IEnumerable<T>> GetEntities()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> PostEntity(T entity)
        {
            var result = false;
            _context.Set<T>().Add(entity);
            result = await _context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> UpdateEntity(T entity)
        {
            var result = false;
            _context.Set<T>().Update(entity);
            result = await _context.SaveChangesAsync() > 0;
            return true;
        }
    }
}
