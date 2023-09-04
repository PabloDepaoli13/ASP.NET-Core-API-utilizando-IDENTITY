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
    public class CursoController : ControllerBase
    {
        private readonly IGenericRepository<Curso> _repository;
        private readonly IMapper _mapper;
        private readonly ICursoRepository _cursoRepository;
        protected RespuestaAPI _respuestaAPI;

        public CursoController(IGenericRepository<Curso> respository, IMapper mapper, ICursoRepository cursoRepository)
        {
            _repository = respository;
            _mapper = mapper;
            this._respuestaAPI = new RespuestaAPI();
            _cursoRepository = cursoRepository;
        }

        
        [HttpGet]
        [ResponseCache(Duration = 60, NoStore = true, Location = ResponseCacheLocation.None)]

        public async Task<ActionResult<IEnumerable<CursoDTO>>> GetCurso()
        {
            var curso = await _repository.GetEntities();
            if (curso == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de las entidades");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var cursoDTO = _mapper.Map<IEnumerable<CursoDTO>>(curso).ToList();

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = cursoDTO;
            return Ok(_respuestaAPI);

        }

        [ResponseCache(CacheProfileName = "DuracionCorta")]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CursoDTO>> GetByIdCurso(int id)
        {
            var curso = await _repository.GetEntityById(id);
            if (curso == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var cursoDTO = _mapper.Map<CursoDTO>(curso);

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = cursoDTO;
            return Ok(_respuestaAPI);

        }

        [HttpGet("GetWithRelationId/{id}")]

        public async Task<ActionResult<CursoDTO>> GetCursoWithRelationsbyId(int id)
        {
            var cursosWithRelations = await _cursoRepository.GetCursoWithRelations(id);
            if (cursosWithRelations == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("No se pudo cargar la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var cursosDTO = _mapper.Map<CursoDTO>(cursosWithRelations);
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = cursosDTO;
            return Ok(_respuestaAPI);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]

        public async Task<ActionResult> SubirInstructor(CursoCreacionDTO entity)
        {
            var curso = _mapper.Map<Curso>(entity);
            var resultado = await _repository.PostEntity(curso);
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

        public async Task<ActionResult> UpdateInstructor(CursoUpdateDTO cursoUpdate)
        {
            var cursoNuevo = _mapper.Map<Curso>(cursoUpdate);
            var resultado = await _repository.UpdateEntity(cursoNuevo);
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
            _respuestaAPI.Result = cursoUpdate;
            return Ok(_respuestaAPI);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteCurso(int id)
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
