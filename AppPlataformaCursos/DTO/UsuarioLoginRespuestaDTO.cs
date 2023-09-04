using AppPlataformaCursos.Models;
using AppPlataformaCursosIdentity.DTO;
using AppPlataformaCursosIdentity.Models;

namespace AppPlataformaCursos.DTO
{
    public class UsuarioLoginRespuestaDTO
    {
        public UsuarioDatosDTO Usuario { get; set; }

        public string Token { get; set; }
    }
}
