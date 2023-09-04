using AppPlataformaAprendizaje.DTO;
using AppPlataformaCursos.DAL.Interfaces;
using AppPlataformaCursos.DTO;
using AppPlataformaCursos.Models;
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
    public class LeccionController : ControllerBase
    {
        private readonly IGenericRepository<Leccion> _repository;
        private readonly IMapper _mapper;
        private readonly ILeccionesRepository _leccionesRepository;
        protected RespuestaAPI _respuestaAPI;

        public LeccionController(IGenericRepository<Leccion> respository, IMapper mapper, ILeccionesRepository leccionesRepository)
        {
            _repository = respository;
            _mapper = mapper;
            this._respuestaAPI = new RespuestaAPI();
            _leccionesRepository = leccionesRepository;
        }

        
        [HttpGet]

        public async Task<ActionResult<IEnumerable<LeccionesDTO>>> GetLecciones()
        {
            var lecciones = await _repository.GetEntities();
            if (lecciones == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de las entidades");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var leccionesDTO = _mapper.Map<IEnumerable<LeccionesDTO>>(lecciones).ToList();

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = leccionesDTO;
            return Ok(_respuestaAPI);

        }

        [ResponseCache(CacheProfileName = "DuracionCorta")]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<LeccionesDTO>> GetByIdLeccion(int id)
        {
            var lecciones = await _repository.GetEntityById(id);
            if (lecciones == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var leccionesDTO = _mapper.Map<LeccionesDTO>(lecciones);

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = leccionesDTO;
            return Ok(_respuestaAPI);

        }

        [HttpGet("GetWithRelation")]

        public async Task<ActionResult<IEnumerable<LeccionesDTO>>> GetLeccionWithRelation()
        {
            var leccion = await _leccionesRepository.GetLeccionWithRelation();
            if (leccion == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var leccionDTO = _mapper.Map<IEnumerable<LeccionesDTO>>(leccion);
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = leccionDTO;
            return Ok(_respuestaAPI);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]

        public async Task<ActionResult> SubirLeccion(LeccionesCreacionDTO entity)
        {
            var leccion = _mapper.Map<Leccion>(entity);
            var resultado = await _repository.PostEntity(leccion);
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

        public async Task<ActionResult> UpdateLeccion(LeccionUpdateDTO leccionUpdate)
        {
            var leccionNuevo = _mapper.Map<Leccion>(leccionUpdate);
            var resultado = await _repository.UpdateEntity(leccionNuevo);
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
            _respuestaAPI.Result = leccionUpdate;
            return Ok(_respuestaAPI);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteLeccion(int id)
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
