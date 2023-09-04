using AppPlataformaCursos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppPlataformaCursos.DTO
{
    public class ComentarioCreacionDTO
    {

        public string Contenido { get; set; }

        public bool Recomendar { get; set; }

        public int IdCurso { get; set; }

    }
}
