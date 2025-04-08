using Microsoft.EntityFrameworkCore;
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

// Inyectar dependencias
builder.Services.AddScoped<ClasesCliente>();
builder.Services.AddScoped<ClasesCita>();

// Configurar CORS
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

// Configurar Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar CORS
app.UseCors("NuevaPolitica");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


// -----------------------------
// Endpoints de Clientes
// -----------------------------

app.MapPost("Guardar", (Cliente dato, ClasesCliente dao) =>
{
    return dao.Guardar(dato);
}).WithTags("Clientes");

app.MapDelete("EliminarCliente", (int id, ClasesCliente dao) =>
{
    return dao.EliminarCliente(id);
}).WithTags("Clientes");

app.MapGet("MostrarClientePorId", (int id, ClasesCliente dao) =>
{
    return dao.MostrarClientePorId(id);
}).WithTags("Clientes");

app.MapGet("MostrarClientes", (ClasesCliente dao) =>
{
    return dao.MostrarClientes();
}).WithTags("Clientes");

app.MapPut("ActualizarCliente", (Cliente dato, ClasesCliente dao) =>
{
    return dao.ActualizarCliente(dato);
}).WithTags("Clientes");


// -----------------------------
// Endpoints de Citas
// -----------------------------

app.MapPost("GuardarCita", (Cita dato, ClasesCita dao) =>
{
    return dao.PostCita(dato);
}).WithTags("Citas");

app.MapGet("ObtenerCitaPorId/{id}", (int id, ClasesCita dao) =>
{
    return dao.GetCita(id);
}).WithTags("Citas");

app.MapGet("ListarCitas", (ClasesCita dao) =>
{
    return dao.GetCitas();
}).WithTags("Citas");

app.MapPut("ActualizarCita", (Cita dato, ClasesCita dao) =>
{
    return dao.PutCita(dato);
}).WithTags("Citas");

app.MapDelete("EliminarCita/{id}", (int id, ClasesCita dao) =>
{
    return dao.DeleteCita(id);
}).WithTags("Citas");


app.Run();

x