using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubscriptoAplicacion.Consumers;
using SubscriptoAplicacion.Data; // Esto asegura que el contexto de la base de datos esté disponible
using System.Threading;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Agregar DbContext para SQL Server usando la cadena de conexión en appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar MassTransit con RabbitMQ para SubscriptoAplicacion
builder.Services.AddMassTransit(x =>
{
    // Registrar el consumidor
    x.AddConsumer<MyConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("admin");
            h.Password("admin123");
        });

        // Configurar la cola y asociarla al consumidor
        cfg.ReceiveEndpoint("my_queue", e =>
        {
            e.ConfigureConsumer<MyConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
