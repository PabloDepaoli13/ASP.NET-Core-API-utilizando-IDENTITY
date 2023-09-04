using AppPlataformaAprendizaje.DAL.Implementations;
using AppPlataformaCursos.DAL.DataContext;
using AppPlataformaCursos.DAL.Interfaces;
using AppPlataformaCursos.Models;
using Microsoft.EntityFrameworkCore;

namespace AppPlataformaCursos.DAL.Implementations
{
    public class InstructorRepository : GenericRepository<Instructor> , IInstructorRepository
    {
        private readonly AplicationDbContext _context;
        public InstructorRepository(AplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Instructor>> GetInstructorWithRelation()
        {
            var query = await _context.Instructors.
                                        Include(e => e.Curso).
                                        ToListAsync();

            return query;
        }
    }
}

