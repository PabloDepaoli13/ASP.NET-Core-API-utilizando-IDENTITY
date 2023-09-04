using AppPlataformaCursos.Models;

namespace AppPlataformaCursos.DAL.Interfaces
{
    public interface ILeccionesRepository : IGenericRepository<Leccion>
    {
        Task<IEnumerable<Leccion>> GetLeccionWithRelation();
    }
}
