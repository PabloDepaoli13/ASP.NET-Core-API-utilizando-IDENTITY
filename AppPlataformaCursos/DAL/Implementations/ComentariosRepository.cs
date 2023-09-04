using AppPlataformaAprendizaje.DAL.Implementations;
using AppPlataformaCursos.DAL.DataContext;
using AppPlataformaCursos.DAL.Interfaces;
using AppPlataformaCursos.DTO;
using AppPlataformaCursos.Models;
using Microsoft.EntityFrameworkCore;

namespace AppPlataformaCursos.DAL.Implementations
{
    public class ComentariosRepository : GenericRepository<Comentarios>, IComentarioRepository
    {
        private readonly AplicationDbContext _context;
        public ComentariosRepository(AplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comentarios>> GetAllComentariesWithRelacion()
        {
            var query = await _context.Comentarios.Include(e => e.Curso).ToListAsync();

            return query;
        }
    }
}
