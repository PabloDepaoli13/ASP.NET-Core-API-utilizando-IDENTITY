using AppPlataformaAprendizaje.DAL.Implementations;
using AppPlataformaCursos.DAL.DataContext;
using AppPlataformaCursos.DAL.Interfaces;
using AppPlataformaCursos.Models;
using Microsoft.EntityFrameworkCore;

namespace AppPlataformaCursos.DAL.Implementations
{
    public class EstudiantesRepository : GenericRepository<Estudiante>, IEstudiantesRepository
    {

        private readonly AplicationDbContext _context;
        public EstudiantesRepository(AplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudiante>> GetEstudiantesWithRelations()
        {
            var query = await _context.Estudiantees.Include(e => e.Curso).ToListAsync();

            return query;
        }
    }
}
