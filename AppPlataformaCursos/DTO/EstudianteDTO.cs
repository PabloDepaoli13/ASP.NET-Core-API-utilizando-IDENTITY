using AppPlataformaCursos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppPlataformaCursos.DTO
{
    public class EstudianteDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Dni { get; set; }

        public string NombreCurso { get; set; }

    }
}
