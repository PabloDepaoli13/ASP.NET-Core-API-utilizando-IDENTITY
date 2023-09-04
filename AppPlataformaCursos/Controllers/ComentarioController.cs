using AppPlataformaCursos.DAL.Interfaces;
using AppPlataformaCursos.DTO;
using AppPlataformaCursos.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppPlataformaCursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IGenericRepository<Comentarios> _repository;
        private readonly IMapper _mapper;
        private readonly IComentarioRepository _comentarioRepository;
        protected RespuestaAPI _respuestaAPI; 

        public ComentarioController(IGenericRepository<Comentarios> respository, IMapper mapper, IComentarioRepository comentarioRepository)
        {
            _repository = respository;
            _mapper = mapper;
            this._respuestaAPI = new RespuestaAPI();
            _comentarioRepository = comentarioRepository;
        }

        
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<ComentarioDTO>>> GetComentarios()
        {
            var comentarios = await _repository.GetEntities();
            if(comentarios == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de las entidades");
                _respuestaAPI.IsSuccess= false;
                return BadRequest(_respuestaAPI);
            }
            var comentariosDTO = _mapper.Map<IEnumerable<ComentarioDTO>>(comentarios).ToList();

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = comentariosDTO;
            return Ok(_respuestaAPI);

        }

        [HttpGet("GetById/{id}")]
        [ResponseCache(CacheProfileName = "DuracionCorta")]
        public async Task<ActionResult<ComentarioDTO>> GetByIdComentario(int id)
        {
            var comentario = await _repository.GetEntityById(id);
            if (comentario == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var comentariosDTO = _mapper.Map<ComentarioDTO>(comentario);

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = comentariosDTO;
            return Ok(_respuestaAPI);

        }

        [HttpGet("ComentariesWithRelation")]

        public async Task<ActionResult<IEnumerable<ComentarioDTO>>> GetComentariesWithRelation()
        {
            var comentarios = await _comentarioRepository.GetAllComentariesWithRelacion();
            if (comentarios == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var comentariosDTO = _mapper.Map<IEnumerable<ComentarioDTO>>(comentarios);
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = comentariosDTO;
            return Ok(_respuestaAPI);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]

        public async Task<ActionResult> SubirComentarios(ComentarioCreacionDTO entity)
        {
            var comentarios = _mapper.Map<Comentarios>(entity);
            var resultado = await _repository.PostEntity(comentarios);
            if (!resultado)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("No se pudo cargar la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
           
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = entity;
            return Ok(_respuestaAPI);
            
        }

        [HttpPut]

        public async Task<ActionResult> UpdateComentario(ComentarioUpdateDTO cometarioUpdate)
        {
            var ComentarioNuevo = _mapper.Map<Comentarios>(cometarioUpdate);
            var resultado = await _repository.UpdateEntity(ComentarioNuevo);
            if (!resultado)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("No se pudo actualizar la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = cometarioUpdate;
            return Ok(_respuestaAPI);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteComentario(int id)
        {
            var resultado = await _repository.DeleteEntity(id);
            if (!resultado)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("No se pudo eliminar la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            return Ok(_respuestaAPI);
        }

    }
}
