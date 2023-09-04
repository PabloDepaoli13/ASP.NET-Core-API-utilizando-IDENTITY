using AppPlataformaCursos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppPlataformaAprendizaje.DTO
{
    public class CursoCreacionDTO
    {

        public string Nombre { get; set; }

        public string Precio { get; set; }

        public string Duracion { get; set; }

        public string FechaInicio { get; set; }

        public int InstructorID { get; set; }


    }
}
