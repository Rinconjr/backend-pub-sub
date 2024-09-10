// PublicadorAplication/Controllers/MessagesController.cs
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CommunWork.Mensajes; // Importar la clase de mensaje desde CommonWork

[ApiController]
[Route("api/messages")]
public class MessagesController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessagesController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] MyMessage message)
    {
        // Publica el mensaje en RabbitMQ
        await _publishEndpoint.Publish(message);
        return Ok("Mensaje enviado correctamente.");
    }
}
