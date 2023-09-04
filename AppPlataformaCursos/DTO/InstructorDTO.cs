using AppPlataformaCursos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AppPlataformaCursos.DTO;

namespace AppPlataformaAprendizaje.DTO
{
    public class InstructorDTO
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string FechaNac { get; set; }
        
        public HashSet<CursoRelacionDTO> Curso { get; set; } = new HashSet<CursoRelacionDTO>();
    }
}
