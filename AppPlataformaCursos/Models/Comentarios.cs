using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppPlataformaCursos.Models
{
    public class Comentarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Contenido { get; set; }

        public bool Recomendar { get; set; }

        public int IdCurso { get; set; }

        [ForeignKey("IdCurso")]

        public Curso Curso { get; set; } = null!;
    }
}
