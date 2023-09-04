using AppPlataformaCursos.Models;

namespace AppPlataformaCursos.DAL.Interfaces
{
    public interface IEstudiantesRepository : IGenericRepository<Estudiante>
    {
        Task<IEnumerable<Estudiante>> GetEstudiantesWithRelations();
    }
}
