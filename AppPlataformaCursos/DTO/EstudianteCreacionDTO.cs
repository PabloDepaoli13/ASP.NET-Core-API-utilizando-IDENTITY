using AppPlataformaCursos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppPlataformaCursos.DTO
{
    public class EstudianteCreacionDTO
    { 

        public string Nombre { get; set; }

        public string Dni { get; set; }

        public int IdCurso { get; set; }

    }
}
