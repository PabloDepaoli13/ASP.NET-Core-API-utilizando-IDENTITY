using AppPlataformaCursos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppPlataformaCursos.DTO
{
    public class ComentarioDTO
    {
        public int Id { get; set; }

        public string Contenido { get; set; }

        public bool Recomendar { get; set; }

        public string NombreCurso { get; set; }

    }
}
