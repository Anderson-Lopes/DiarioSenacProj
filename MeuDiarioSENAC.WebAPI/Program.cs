using MeuDiarioSENAC.Model;
using MeuDiarioSENAC.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options => 
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.
    IgnoreCycles;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors("Frontend");
var registrosGroup = app.MapGroup("/registros");

app.MapGet("/", () => "Hello World");
app.MapGet("/motivacional", () => "Hello World! Mundo cruel onde vivemos. E cada integrante deste curso abusa consideravelmente da paciência alheia.");
registrosGroup.MapGet("/", () =>
    {
        List<Registro> registros = new RegistroService().ListarTodos();
        return registros;
    });
registrosGroup.MapPost("/", ([FromBody] Registro registro) =>
    {
        bool inserido = new RegistroService().InserirRegistro(registro);
        return inserido
            ? Results.Ok("Registro inserido com sucesso")
            : Results.BadRequest("Registro inválido");
    });
registrosGroup.MapPut("/{id}", ([FromRoute] int id, [FromBody] Registro registro) =>
    {
        registro.Id = id;
        bool atualizado = new RegistroService().Atualizar(registro);
        return atualizado
            ? Results.Ok("Registro atualizado com sucesso")
            : Results.BadRequest("Registro inválido ou não encontrado");
    });

registrosGroup.MapDelete("/{id}", ([FromRoute] int id) =>
    {
        bool excluido = new RegistroService().Excluir(id);
        return excluido
            ? Results.Ok("Registro excluído com sucesso")
            : Results.BadRequest("Registro não encontrado");
    });
registrosGroup.MapGet("/{id}", ([FromRoute] int id) =>
    {
        Registro? registro = new RegistroService().BuscarPorId(id);
        return registro is not null
            ? Results.Ok(registro)
            : Results.NotFound("Registro não encontrado");
    });

app.Run();
