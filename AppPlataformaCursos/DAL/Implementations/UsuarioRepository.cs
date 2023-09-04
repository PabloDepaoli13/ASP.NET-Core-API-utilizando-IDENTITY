using AppPlataformaAprendizaje.DAL.Implementations;
using AppPlataformaAprendizaje.DTO;
using AppPlataformaCursos.DAL.DataContext;
using AppPlataformaCursos.DAL.Interfaces;
using AppPlataformaCursos.DTO;
using AppPlataformaCursos.Models;
using AppPlataformaCursosIdentity.DTO;
using AppPlataformaCursosIdentity.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XAct.Users;
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

        public async Task<string> UpdateUsuario(UsuarioUpdateDTOIdentity usuario)
        {
            var message = "";
            var usuarioViejo = await _context.AppUsuarios.FindAsync(usuario.Id);
            if (usuarioViejo == null)
            {
                message = "No se encontro el usuario, por favor revise el id";
                return message;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(usuarioViejo);
            var resetPass = await _userManager.ResetPasswordAsync(usuarioViejo, token, usuario.Password);

            if (!resetPass.Succeeded)
            {
                message = "No se logro actualizar la contraseña, error interno del servidor";
                return message;
            }

            usuarioViejo.Email = usuario.NombreUsuario ?? usuarioViejo.Email;
            usuarioViejo.Nombre = usuario.Nombre ?? usuarioViejo.Nombre;
            usuarioViejo.NormalizedEmail = usuario.NombreUsuario.ToUpper() ?? usuarioViejo.NormalizedEmail;

            await _userManager.UpdateAsync(usuarioViejo);

            var result = await _context.SaveChangesAsync() > 0;
            message = "Actualizacion procesada con exito";

            return message;
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

        public async Task<bool> EliminarUsuario(string nombreUsuario)
        {
            var result = false;
            var usuario = await _context.AppUsuarios.FirstOrDefaultAsync(e => e.Id == nombreUsuario);
            _context.AppUsuarios.Remove(usuario);
            result = await _context.SaveChangesAsync() > 0;
            return result;

        }
    }
}
