using AppPlataformaAprendizaje.DTO;
using AppPlataformaCursos.DAL.Interfaces;
using AppPlataformaCursos.DTO;
using AppPlataformaCursos.Models;
using AppPlataformaCursosIdentity.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace AppPlataformaCursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IGenericRepository<AppUsuarios> _repository;
        private readonly IUsuarioRepository _userRepository;
        private readonly IMapper _mapper;
        protected RespuestaAPI respuestaAPI;

        public UsuarioController(IGenericRepository<AppUsuarios> repository, IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = usuarioRepository;
            this.respuestaAPI = new RespuestaAPI();
        }

        
        [HttpGet]
        [ResponseCache(Duration = 60, NoStore = true, Location = ResponseCacheLocation.None)]

        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerUsuariosDisponibles()
        {
            var usuarios = await _repository.GetEntities();
            if (usuarios == null)
            {
                respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                respuestaAPI.ErrorMessage.Add("Error en la obtencion de las entidades");
                respuestaAPI.IsSuccess = false;
                return BadRequest(respuestaAPI);
            }

            respuestaAPI.StatusCode = HttpStatusCode.OK;
            respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            respuestaAPI.IsSuccess = true;
            respuestaAPI.Result = usuarios;
            return Ok(respuestaAPI);
        }

        [HttpGet("GetById/{id}")]

        public async Task<ActionResult<UsuarioDTO>> GetUserById(string id)
        {
            var usuarios = await _userRepository.GetUserById(id);
            if (usuarios == null)
            {
                respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                respuestaAPI.ErrorMessage.Add("Usuario no encontrado: Coloque una Id valida");
                respuestaAPI.IsSuccess = false;
                return BadRequest(respuestaAPI);
            }
            var usuariosDTO = _mapper.Map<UsuarioDTO>(usuarios);
            respuestaAPI.StatusCode = HttpStatusCode.OK;
            respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            respuestaAPI.IsSuccess = true;
            respuestaAPI.Result = usuariosDTO;
            return Ok(respuestaAPI);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]

        public async Task<ActionResult> RegistroUsuario([FromBody]UsuarioRegistroDTO usuario)
        {
            var usuarioIsUnique = await _userRepository.UniqueUsuario(usuario.NombreUsuario);
            if (!usuarioIsUnique)
            {
                respuestaAPI.StatusCode = HttpStatusCode.Conflict;
                respuestaAPI.ErrorMessage.Add("Error: El nombre de usuario no esta disponible");
                respuestaAPI.IsSuccess = false;
                return BadRequest(respuestaAPI);
            }
            var resultado = await _userRepository.RegisterUsuario(usuario);
            if (!resultado)
            {
                respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                respuestaAPI.ErrorMessage.Add("Error: No se pudo añadir la entidad en la base de datos");
                respuestaAPI.IsSuccess = false;
                return BadRequest(respuestaAPI);
            }
            
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            respuestaAPI.StatusCode = HttpStatusCode.OK;
            respuestaAPI.ErrorMessage.Add("Usuario cargado con exito");
            respuestaAPI.IsSuccess = true;
            respuestaAPI.Result = usuarioDTO;
            return Ok(respuestaAPI);


        }

        [HttpPost("Login")]

        public async Task<IActionResult> LoginUser(UsuarioLoginDTO usuario)
        {
            var usuarioLogin = await _userRepository.Login(usuario);
            if (usuarioLogin.Usuario == null || usuarioLogin.Token == "")
            {
                respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                respuestaAPI.ErrorMessage.Add("Error: Usuario o contraseñas son incorrectos");
                respuestaAPI.IsSuccess = false;
                return BadRequest(respuestaAPI);
            }
            respuestaAPI.StatusCode = HttpStatusCode.OK;
            respuestaAPI.ErrorMessage.Add("Inicio de sesion exitoso");
            respuestaAPI.IsSuccess = true;
            respuestaAPI.Result = usuarioLogin;
            return Ok(respuestaAPI);
        }

        [HttpDelete]

        public async Task<ActionResult> EliminarUsuarios(int id)
        {
            var resultado = await _repository.DeleteEntity(id);
            if (!resultado)
            {
                respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                respuestaAPI.ErrorMessage.Add("Error del servidor, no se pudo eliminar el usuario");
                respuestaAPI.IsSuccess = false;
                return BadRequest(respuestaAPI);
            }
            respuestaAPI.StatusCode = HttpStatusCode.OK;
            respuestaAPI.ErrorMessage.Add("Eliminado con exito");
            respuestaAPI.IsSuccess = true;
            return Ok(respuestaAPI);
        }

        [HttpPut]

        public async Task<ActionResult> UpdateComentario(Usuario usuarioUpdate)
        {
            
            var resultado = await _userRepository.UpdateUsuario(usuarioUpdate);
            if (!resultado)
            {
                respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                respuestaAPI.ErrorMessage.Add("No se pudo actualizar la entidad");
                respuestaAPI.IsSuccess = false;
                return BadRequest(respuestaAPI);
            }
            respuestaAPI.StatusCode = HttpStatusCode.OK;
            respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            respuestaAPI.IsSuccess = true;
            respuestaAPI.Result = usuarioUpdate;
            return Ok(respuestaAPI);
        }




    }
}
