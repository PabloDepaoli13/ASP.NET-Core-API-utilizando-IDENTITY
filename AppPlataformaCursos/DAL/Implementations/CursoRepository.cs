using AppPlataformaAprendizaje.DAL.Implementations;
using AppPlataformaCursos.DAL.DataContext;
using AppPlataformaCursos.DAL.Interfaces;
using AppPlataformaCursos.Models;
using Microsoft.EntityFrameworkCore;

namespace AppPlataformaCursos.DAL.Implementations
{
    public class CursoRepository : GenericRepository<Curso>, ICursoRepository
    {
        private readonly AplicationDbContext _context;
        public CursoRepository(AplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Curso> GetCursoWithRelations(int id)
        {
            var query = await _context.Cursos.
                                 Include(e => e.Instructor).
                                 Include(e => e.Lecciones).
                                 Include(e => e.Comentarios).
                                 Include(e => e.Estudiante).
                                 FirstOrDefaultAsync(e => e.Id == id);

            return query;
        }
    }
}
