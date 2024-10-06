//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

//******************************************************
//////using Infrastructure;
//////using Microsoft.Extensions.DependencyInjection;
//////using Microsoft.OpenApi.Models;
//////using PersonCatalog.Middlewares;
//////using Microsoft.EntityFrameworkCore;
//////using System.Reflection;
//////using Application.Persons.Commands;
//////using MediatR;

//////var builder = WebApplication.CreateBuilder(args);

//////// Agregar EF Core y MediatR
////////builder.Services.AddDbContext<ApplicationDbContext>(options =>
////////    options.UseMySql(
////////        builder.Configuration.GetConnectionString("DefaultConnection"),
////////        new MySqlServerVersion(new Version(8, 0, 21))
////////    ));

//////// Add services to the container.
//////builder.Services.AddDbContext<ApplicationDbContext>(options =>
//////    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

////////builder.Services.AddMediatR(typeof(Application.Persons.Commands.CreatePersonCommand).Assembly);

//////// Register MediatR
//////builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreatePersonCommand).Assembly);

//////// Swagger
//////builder.Services.AddSwaggerGen(c =>
//////{
//////    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Person API", Version = "v1" });
//////});

//////var app = builder.Build();

//////app.UseMiddleware<ExceptionMiddleware>(); // Middleware para manejo de excepciones

//////if (app.Environment.IsDevelopment())
//////{
//////    app.UseSwagger();
//////    app.UseSwaggerUI(c =>
//////    {
//////        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Person API v1");
//////    });
//////}

//////app.UseHttpsRedirection();
//////app.UseAuthorization();
//////app.MapControllers();
//////app.Run();

//*******************************************************************


using Application.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.OpenApi.Models;
using PersonCatalog.Middlewares;
using System.Reflection;
using Application.Persons.Commands;
///
///
var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework Core con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Registrar el repositorio
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

// Configurar MediatR (versión 9.0.0)
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Application.Persons.Commands.CreatePersonCommand).Assembly));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreatePersonCommand).Assembly);


// Agregar controladores
builder.Services.AddControllers();

// Configurar Swagger para la documentación de la API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Person Catalog API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")  // Reemplaza con el origen de tu frontend
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Construir la aplicación
var app = builder.Build();
// Habilitar CORS para toda la aplicación
app.UseCors("AllowSpecificOrigins");

// Middleware de manejo de excepciones
app.UseMiddleware<ExceptionMiddleware>();

// Middleware para Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Person Catalog API v1");
    });
}

// Otras configuraciones de middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Mapear los controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();