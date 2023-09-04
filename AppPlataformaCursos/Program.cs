using AppPlataformaAprendizaje.DAL.Implementations;
using AppPlataformaCursos.DAL.DataContext;
using AppPlataformaCursos.DAL.Implementations;
using AppPlataformaCursos.DAL.Interfaces;
using AppPlataformaCursos.Utilities;
using AppPlataformaCursosIdentity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.CacheProfiles.Add("DuracionCorta", new CacheProfile() 
    {
        Duration = 40 
    });
});

builder.Services.AddMemoryCache();

builder.Services.AddResponseCaching(opt =>
{
    opt.SizeLimit= 1000;
    opt.UseCaseSensitivePaths= true;
   
});



builder.Services.AddIdentity<AppUsuarios, IdentityRole>().AddEntityFrameworkStores<AplicationDbContext>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Para acceder a nuestro servicio de registro necesitas loggearte.\r\n\r\n" +
                      "Debes escribir Bearer seguido de [espacio] y el token de tu usuario.\r\n\r\n" +
                      "Ejemplo : Bearer euiwhqguidnjglhubyowadlcvmxclvilequieryoiquhfdmsbfasd",
        Name = "Authorization",
        Scheme = "Bearer",
        In = ParameterLocation.Header

    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});



builder.Services.AddDbContext<AplicationDbContext>(e => e.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<ICursoRepository,CursoRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IComentarioRepository, ComentariosRepository>();
builder.Services.AddScoped<ILeccionesRepository, LeccionRepository>();
builder.Services.AddScoped<IEstudiantesRepository, EstudiantesRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var key = builder.Configuration.GetValue<string>("APIConfig:ClaveSecreta");

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(e =>
{
    e.RequireHttpsMetadata = false;
    e.SaveToken = true;
    e.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddCors(opt => opt.AddPolicy("PolicyCors", build => {

    build.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();

}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.UseCors("PolicyCors");

app.Run();
