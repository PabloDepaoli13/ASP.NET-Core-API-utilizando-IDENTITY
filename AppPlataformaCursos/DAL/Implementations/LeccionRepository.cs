using AppPlataformaAprendizaje.DAL.Implementations;
using AppPlataformaCursos.DAL.DataContext;
using AppPlataformaCursos.DAL.Interfaces;
using AppPlataformaCursos.Models;
using Microsoft.EntityFrameworkCore;

namespace AppPlataformaCursos.DAL.Implementations
{
    public class LeccionRepository : GenericRepository<Leccion>, ILeccionesRepository
    {
        private readonly AplicationDbContext _context;
        public LeccionRepository(AplicationDbContext context) : base(context)
        {
            _context= context;
        }

        public async Task<IEnumerable<Leccion>> GetLeccionWithRelation()
        {
            var query = await _context.Lecciones.Include(e => e.Curso).ToListAsync();

            return query;
        }
    }
}
