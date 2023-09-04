using AppPlataformaAprendizaje.DTO;
using AppPlataformaCursos.DAL.Implementations;
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
    public class EstudianteController : ControllerBase
    {
        private readonly IGenericRepository<Estudiante> _repository;
        private readonly IMapper _mapper;
        private readonly IEstudiantesRepository _estudiantesRepository;
        protected RespuestaAPI _respuestaAPI;

        public EstudianteController(IGenericRepository<Estudiante> respository, IMapper mapper, IEstudiantesRepository estudiantesRepository)
        {
            _repository = respository;
            _mapper = mapper;
            this._respuestaAPI = new RespuestaAPI();
            _estudiantesRepository = estudiantesRepository;
        }

        
        [HttpGet]
        [ResponseCache(Duration = 60, NoStore = true, Location = ResponseCacheLocation.None)]

        public async Task<ActionResult<IEnumerable<EstudianteDTO>>> GetEstudiantes()
        {
            var estudiantes = await _repository.GetEntities();
            if (estudiantes == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de las entidades");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var estudiantesDTO = _mapper.Map<IEnumerable<EstudianteDTO>>(estudiantes).ToList();

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = estudiantesDTO;
            return Ok(_respuestaAPI);

        }

        [ResponseCache(CacheProfileName = "DuracionCorta")]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<EstudianteDTO>> GetByIdEstudiante(int id)
        {
            var estudiantes = await _repository.GetEntityById(id);
            if (estudiantes == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var estudiantesDTO = _mapper.Map<EstudianteDTO>(estudiantes);

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = estudiantesDTO;
            return Ok(_respuestaAPI);

        }


        [HttpGet("WithRelations")]

        public async Task<ActionResult<IEnumerable<EstudianteDTO>>> GetEstudiantesWithRelations()
        {
            var estudiante = await _estudiantesRepository.GetEstudiantesWithRelations();
            if (estudiante == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var estudianteDTO = _mapper.Map<IEnumerable<EstudianteDTO>>(estudiante);
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = estudianteDTO;
            return Ok(_respuestaAPI);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]

        public async Task<ActionResult> SubirEstudiante(EstudianteCreacionDTO entity)
        {
            var estudiante = _mapper.Map<Estudiante>(entity);
            var resultado = await _repository.PostEntity(estudiante);
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

        public async Task<ActionResult> UpdateEstudiante(EstudianteUpdateDTO estudianteUpdate)
        {
            var EstudianteNuevo = _mapper.Map<Estudiante>(estudianteUpdate);
            var resultado = await _repository.UpdateEntity(EstudianteNuevo);
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
            _respuestaAPI.Result = estudianteUpdate;
            return Ok(_respuestaAPI);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteEstudiante(int id)
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
