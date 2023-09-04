using Microsoft.AspNetCore.Identity;

namespace AppPlataformaCursosIdentity.Models
{
    public class AppUsuarios : IdentityUser
    {
        public string Nombre { get; set; }
    }
}
