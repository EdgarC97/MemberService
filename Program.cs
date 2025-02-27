// Program.cs (versión actualizada)
using Microsoft.EntityFrameworkCore;
using MemberService.Data;
using MemberService.Repositories;
using MemberService.Repositories.Interfaces;
using MemberService.Services;
using MemberService.Services.Interfaces;
using MemberService.Mappers;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Determinar el entorno (puedes usar un argumento o verificar alguna variable de entorno)
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

// Cargar el archivo .env correspondiente
if (env == "Development")
{
    Env.Load(".env.local");
}
else
{
    Env.Load(".env.production");
}

// Añadir servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Obtener el connection string desde variables de entorno
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

// Configurar el DbContext con el connection string
builder.Services.AddDbContext<MemberDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registrar servicios y repositorios
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberManagementService>();

// Configuración de AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure el pipeline de solicitudes HTTP
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Aplicar migraciones automáticamente en desarrollo
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<MemberDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();
    }
//}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();