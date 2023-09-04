using AppPlataformaCursos.Models;

namespace AppPlataformaCursos.DAL.Interfaces
{
    public interface ICursoRepository : IGenericRepository<Curso>
    {
        Task<Curso> GetCursoWithRelations(int id);


    }
}
