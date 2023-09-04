using AppPlataformaCursos.DTO;
using AppPlataformaCursos.Models;

namespace AppPlataformaCursos.DAL.Interfaces
{
    public interface IComentarioRepository : IGenericRepository<Comentarios>
    {
        Task<IEnumerable<Comentarios>> GetAllComentariesWithRelacion();
    }
}
