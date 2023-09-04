using AppPlataformaAprendizaje.DTO;
using AppPlataformaCursos.DTO;
using AppPlataformaCursos.Models;
using AppPlataformaCursosIdentity.DTO;
using AppPlataformaCursosIdentity.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppPlataformaCursos.DAL.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuario);

        Task<bool> RegisterUsuario(UsuarioRegistroDTO usuario);

        Task<bool> UniqueUsuario(string nombreUsuario);

        Task<string> UpdateUsuario(UsuarioUpdateDTOIdentity usuario);

        Task<AppUsuarios> GetUserById(string usuarioId);

        Task<bool> EliminarUsuario(string nombreUsuario);
    }
}
