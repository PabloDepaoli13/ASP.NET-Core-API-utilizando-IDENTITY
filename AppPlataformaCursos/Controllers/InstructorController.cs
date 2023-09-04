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
    public class InstructorController : ControllerBase
    {
        private readonly IGenericRepository<Instructor> _repository;
        private readonly IMapper _mapper;
        private readonly IInstructorRepository _instructorRepository;
        protected RespuestaAPI _respuestaAPI;

        public InstructorController(IGenericRepository<Instructor> respository, IMapper mapper, IInstructorRepository instructorRepository)
        {
            _repository = respository;
            _mapper = mapper;
            this._respuestaAPI = new RespuestaAPI();
            _instructorRepository = instructorRepository;
        }

        
        [HttpGet]
        

        public async Task<ActionResult<IEnumerable<InstructorDTO>>> GetInstructor()
        {
            var instructor = await _repository.GetEntities();
            if (instructor == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de las entidades");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var instructorDTO = _mapper.Map<IEnumerable<InstructorDTO>>(instructor).ToList();

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = instructorDTO;
            return Ok(_respuestaAPI);

        }

        [ResponseCache(CacheProfileName = "DuracionCorta")]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<InstructorDTO>> GetByIdInstructor(int id)
        {
            var instructor = await _repository.GetEntityById(id);
            if (instructor == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var instructorDTO = _mapper.Map<InstructorDTO>(instructor);

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = instructorDTO;
            return Ok(_respuestaAPI);

        }

        [HttpGet("WithRelations")]

        public async Task<ActionResult<IEnumerable<InstructorDTO>>> GetInstructorsWithRelations()
        {
            var instructors = await _instructorRepository.GetInstructorWithRelation();
            if (instructors == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.ErrorMessage.Add("Error en la obtencion de la entidad");
                _respuestaAPI.IsSuccess = false;
                return BadRequest(_respuestaAPI);
            }
            var instructorsDTO = _mapper.Map<IEnumerable<InstructorDTO>>(instructors);
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.ErrorMessage.Add("Operacion exitosa");
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = instructorsDTO;
            return Ok(_respuestaAPI);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]

        public async Task<ActionResult> SubirInstructor(InstructorCreacionDTO entity)
        {
            var instructor = _mapper.Map<Instructor>(entity);
            var resultado = await _repository.PostEntity(instructor);
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

        public async Task<ActionResult> UpdateInstructor(InstuctorUpdateDTO instructorUpdate)
        {
            var InstructorNuevo = _mapper.Map<Instructor>(instructorUpdate);
            var resultado = await _repository.UpdateEntity(InstructorNuevo);
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
            _respuestaAPI.Result = instructorUpdate;
            return Ok(_respuestaAPI);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteInstructor(int id)
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
