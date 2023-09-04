using AppPlataformaAprendizaje.DAL.Implementations;
using AppPlataformaAprendizaje.DTO;
using AppPlataformaCursos.DAL.DataContext;
using AppPlataformaCursos.DAL.Interfaces;
using AppPlataformaCursos.DTO;
using AppPlataformaCursos.Models;
using AppPlataformaCursosIdentity.DTO;
using AppPlataformaCursosIdentity.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace AppPlataformaCursos.DAL.Implementations
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private string _claveSecreta;
        private AplicationDbContext _context;
        private readonly UserManager<AppUsuarios> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsuarioRepository(AplicationDbContext context, IConfiguration config, UserManager<AppUsuarios> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper) : base(context)
        {
            _claveSecreta = config.GetValue<string>("APIConfig:ClaveSecreta");
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuario)
        {
            var key = Encoding.ASCII.GetBytes(_claveSecreta);

            var usuarioEncontrado = await _context.AppUsuarios.FirstOrDefaultAsync(e => e.UserName.ToLower() == usuario.NombreUsuario.ToLower());

            var UserValid = await _userManager.CheckPasswordAsync(usuarioEncontrado, usuario.Password);

            if (usuarioEncontrado == null || UserValid == false)
            {
                return new UsuarioLoginRespuestaDTO()
                {
                    Token = "",
                    Usuario = null
                };
            }

            var roles = await _userManager.GetRolesAsync(usuarioEncontrado);

            var manejadorToken = new JwtSecurityTokenHandler();

            var descriptorToken = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name ,  usuarioEncontrado.UserName.ToString()),
                    new Claim(ClaimTypes.Role , roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = manejadorToken.CreateToken(descriptorToken);

            var usuarioLoginRespuestaDTO = new UsuarioLoginRespuestaDTO()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = _mapper.Map<UsuarioDatosDTO>(usuarioEncontrado)
            };

            return usuarioLoginRespuestaDTO;

            
        }

        public async Task<bool> RegisterUsuario(UsuarioRegistroDTO usuario)
        {
            
            var usuarioNuevo = new AppUsuarios()
            {
                UserName = usuario.NombreUsuario,
                Nombre = usuario.Nombre,
                Email = usuario.NombreUsuario,
                NormalizedEmail = usuario.NombreUsuario.ToUpper()
            };

            
            var result = await _userManager.CreateAsync(usuarioNuevo, usuario.Password);

            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("member"));
                }
                await _userManager.AddToRoleAsync(usuarioNuevo, "admin");

                return true;

            }
            else return false;

        }

        public async Task<bool> UniqueUsuario(string nombreUsuario)
        {
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(e => e.NombreUsuario == nombreUsuario);
            if (usuarioEncontrado == null) return true;
            return false;
          
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            var result = false;

            var usuarioViejo = await _context.Usuarios.FindAsync(usuario.Id);

            if (usuarioViejo == null)
            {
                return false;
            }

            var contraseñaEncriptada = "";

            usuarioViejo.NombreUsuario = usuario.NombreUsuario ?? usuarioViejo.NombreUsuario;
            usuarioViejo.Nombre = usuario.Nombre ?? usuarioViejo.Nombre;
            usuarioViejo.Password = contraseñaEncriptada ?? usuarioViejo.Password;
            usuarioViejo.Role = usuario.Role ?? usuarioViejo.Role;

            _context.Entry(usuarioViejo).State = EntityState.Modified;
           
            result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<AppUsuarios> GetUserById(string usuarioId)
        {
            var usuarioEncontrado = await _context.AppUsuarios.FindAsync(usuarioId);
            if (usuarioEncontrado == null)
            {
                return null;
            }
            return usuarioEncontrado;
        }
    }
}
