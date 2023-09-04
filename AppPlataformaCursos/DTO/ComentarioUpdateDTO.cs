namespace AppPlataformaCursos.DTO
{
    public class ComentarioUpdateDTO
    {

        public int Id { get; set; }
        public string Contenido { get; set; }

        public bool Recomendar { get; set; }

        public int IdCurso { get; set; }
    }
}
