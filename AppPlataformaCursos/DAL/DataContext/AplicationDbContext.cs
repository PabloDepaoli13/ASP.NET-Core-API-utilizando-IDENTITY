using AppPlataformaCursos.Models;
using AppPlataformaCursosIdentity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppPlataformaCursos.DAL.DataContext
{
    public class AplicationDbContext : IdentityDbContext<AppUsuarios>
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        
        }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Curso>().Property(e => e.FechaInicio).HasColumnType("date");
            modelBuilder.Entity<Curso>().Property(e => e.Nombre).HasMaxLength(100);


            modelBuilder.Entity<Instructor>().Property(e => e.FechaNac).HasColumnType("date");
            modelBuilder.Entity<Instructor>().Property(e => e.Nombre).HasMaxLength(80);

            modelBuilder.Entity<Leccion>().Property(e => e.Contenido).HasMaxLength(600);

            modelBuilder.Entity<Comentarios>().Property(e => e.Contenido).HasMaxLength(600);


        }
    


        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Comentarios> Comentarios { get; set; }

        public DbSet<Estudiante> Estudiantees { get;set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Leccion> Lecciones { get; set;}

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<AppUsuarios> AppUsuarios { get; set; }
    }
}
