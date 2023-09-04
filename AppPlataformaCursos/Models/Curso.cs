using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppPlataformaCursos.Models
{
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public double Precio { get; set; }

        public string Duracion { get; set; }

        public DateTime FechaInicio { get; set; }

        public int InstructorID { get; set; }

        [ForeignKey("InstructorID")]
        public Instructor Instructor { get; set; } = null!;

        public HashSet<Leccion> Lecciones { get; set; } = new HashSet<Leccion>();

        public HashSet<Estudiante> Estudiante { get; set; } = new HashSet<Estudiante>();

        public HashSet<Comentarios> Comentarios { get; set; }  =   new HashSet<Comentarios>();
    }
}
