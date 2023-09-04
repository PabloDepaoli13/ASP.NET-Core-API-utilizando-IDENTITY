using AppPlataformaCursos.Models;

namespace AppPlataformaCursos.DAL.Interfaces
{
    public interface IInstructorRepository : IGenericRepository<Instructor>
    {
        Task<IEnumerable<Instructor>> GetInstructorWithRelation();
    }
}
