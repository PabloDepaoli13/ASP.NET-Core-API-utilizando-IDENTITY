using AppPlataformaCursos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AppPlataformaCursos.DTO;

namespace AppPlataformaAprendizaje.DTO
{
    public class CursoDTO
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Precio { get; set; }

        public string Duracion { get; set; }

        public string FechaInicio { get; set; }

        public string NombreInstructor { get; set; }

        public HashSet<LeccionesDTO> Lecciones { get; set; } = new HashSet<LeccionesDTO>();

        public HashSet<EstudianteDTO> Estudiante { get; set; } = new HashSet<EstudianteDTO>();

        public HashSet<ComentarioDTO> Comentarios { get; set; } = new HashSet<ComentarioDTO>();
    }
}
