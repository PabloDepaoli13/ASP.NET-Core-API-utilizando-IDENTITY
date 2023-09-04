using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppPlataformaCursos.Models
{
    public class Leccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Contenido { get; set; }

        public int IdCurso { get; set; }

        [ForeignKey("IdCurso")]

        public Curso Curso { get; set; } = null!;

    }
}
