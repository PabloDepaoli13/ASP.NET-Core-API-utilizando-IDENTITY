using System.Net;

namespace AppPlataformaCursos.Models
{
    public class RespuestaAPI
    {
        public RespuestaAPI()
        {
            ErrorMessage = new List<string>();
        }

        public List<string> ErrorMessage { get; set;} = new List<string>();

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }

        public object Result { get; set; } = null!;
    }
}
