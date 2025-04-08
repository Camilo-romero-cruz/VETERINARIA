using Microsoft.EntityFrameworkCore;
using System.Threading;
using VETERINARIA_DB.Clases;
using VETERINARIA_DB.Models;

var builder = WebApplication.CreateBuilder(args);

// Leer la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Agregar el contexto de base de datos
builder.Services.AddDbContext<VeterinariaDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ClasesCliente>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");

app.MapPost("Guardar", (Cliente dato, ClasesCliente dao) =>
{
    return dao.Guardar(dato);
});

app.MapDelete("EliminarCliente", (int id, ClasesCliente dao) =>
{
    return dao.EliminarCliente(id);
});

app.MapGet("MostrarClientePorId", (int id, ClasesCliente dao) =>
{
    return dao.MostrarClientePorId(id);
});

app.MapGet("MostrarClientes", (ClasesCliente dao) =>
{
    return dao.MostrarClientes();
});

// Endpoint para actualizar Cliente
app.MapPut("ActualizarCliente", (Cliente dato, ClasesCliente dao) =>
{
    return dao.ActualizarCliente(dato);
});


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
