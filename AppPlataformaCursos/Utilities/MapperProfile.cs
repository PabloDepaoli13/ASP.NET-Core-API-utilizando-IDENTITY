using AppPlataformaAprendizaje.DTO;
using AppPlataformaCursos.DTO;
using AppPlataformaCursos.Models;
using AppPlataformaCursosIdentity.DTO;
using AppPlataformaCursosIdentity.Models;
using AutoMapper;

namespace AppPlataformaCursos.Utilities
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {

            

            CreateMap<Comentarios, ComentarioDTO>().ForMember(e => e.NombreCurso, opt => opt.MapFrom(e => e.Curso.Nombre));

            CreateMap<ComentarioDTO, Comentarios>();

            CreateMap<Comentarios, ComentarioCreacionDTO>().ReverseMap();

            CreateMap<ComentarioUpdateDTO, Comentarios>().ReverseMap();



            CreateMap<Instructor,InstructorDTO>().ForMember(e => e.FechaNac, opt => opt.MapFrom(e => e.FechaNac.ToString("dd/MM/yyyy")));
            CreateMap<InstructorCreacionDTO, Instructor>().ForMember(e => e.FechaNac, opt => opt.MapFrom(e => DateTime.Parse(e.FechaNac)));
            CreateMap<InstuctorUpdateDTO, Instructor>().ForMember(e => e.FechaNac, opt => opt.MapFrom(e => DateTime.Parse(e.FechaNac)));
            CreateMap<InstructorDTO, Instructor>().ForMember(e => e.FechaNac, opt => opt.MapFrom(e => DateTime.Parse(e.FechaNac)));




            CreateMap<CursoDTO, Curso>().ForMember(e => e.FechaInicio, opt => opt.MapFrom(e => DateTime.Parse(e.FechaInicio))).ForMember(e => e.Precio, opt => opt.MapFrom(e => Double.Parse(e.Precio)));

            CreateMap<CursoCreacionDTO, Curso>().ForMember(e => e.FechaInicio, opt => opt.MapFrom(e => DateTime.Parse(e.FechaInicio))).ForMember(e => e.Precio, opt => opt.MapFrom(e => Double.Parse(e.Precio)));

            CreateMap<CursoUpdateDTO, Curso>().ForMember(e => e.FechaInicio, opt => opt.MapFrom(e => DateTime.Parse(e.FechaInicio))).ForMember(e => e.Precio, opt => opt.MapFrom(e => Double.Parse(e.Precio)));

            CreateMap<Curso, CursoDTO>().ForMember(e => e.FechaInicio, opt => opt.MapFrom(e => e.FechaInicio.ToString("dd/MM/yyyy"))).ForMember(e => e.Precio, opt => opt.MapFrom(e => e.Precio.ToString())).ForMember(e => e.NombreInstructor, opt => opt.MapFrom(e => e.Instructor.Nombre));

            CreateMap<Curso, CursoRelacionDTO>().ForMember(e => e.FechaInicio, opt => opt.MapFrom(e => e.FechaInicio.ToString("dd/MM/yyyy"))).ForMember(e => e.Precio, opt => opt.MapFrom(e => e.Precio.ToString()));





            CreateMap<Estudiante, EstudianteDTO>().ForMember(e => e.NombreCurso, opt => opt.MapFrom(e => e.Curso.Nombre));


            CreateMap<EstudianteDTO, Estudiante>();

            CreateMap<Estudiante, EstudianteCreacionDTO>().ReverseMap();

            CreateMap<Estudiante, EstudianteUpdateDTO>().ReverseMap();




            CreateMap<Leccion, LeccionesDTO>().ForMember(e => e.NombreCurso, opt => opt.MapFrom(e => e.Curso.Nombre));

            CreateMap<LeccionesDTO, Leccion>();

            CreateMap<Leccion, LeccionUpdateDTO>().ReverseMap();

            CreateMap<Leccion, LeccionesCreacionDTO>().ReverseMap();


            CreateMap<Usuario, UsuarioRegistroDTO>().ReverseMap();

            CreateMap<Usuario, UsuarioLoginDTO>().ReverseMap();

            CreateMap<UsuarioRegistroDTO, UsuarioDTO>().ReverseMap();

            CreateMap<AppUsuarios, UsuarioDatosDTO>().ReverseMap();

            CreateMap<AppUsuarios, UsuarioDTO>().ForMember(e => e.Nombre, opt => opt.MapFrom(e => e.Nombre)).ForMember(e => e.NombreUsuario, opt => opt.MapFrom(e => e.Email)).ForMember(e => e.Id, opt => opt.MapFrom(e => e.Id));
        }
    }
}
