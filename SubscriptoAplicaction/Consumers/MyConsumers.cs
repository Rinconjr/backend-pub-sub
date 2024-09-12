using MassTransit;
using CommunWork.Mensajes; // Asegúrate de importar el namespace correcto del mensaje
using SubscriptoAplicacion.Services;
using System.Threading.Tasks;

namespace SubscriptoAplicacion.Consumers
{
    public class MyConsumer : IConsumer<MyMessage>
    {
        private readonly WebSocketConnectionManager _connectionManager;

        public MyConsumer(WebSocketConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public async Task Consume(ConsumeContext<MyMessage> context)
        {
            // Lógica para procesar el mensaje recibido
            string messageText = context.Message.Text;
            Console.WriteLine($"Mensaje recibido: {messageText}");

            // Enviar el mensaje a todos los WebSockets conectados
            await _connectionManager.SendMessageToAllAsync($"Mensaje recibido desde el consumidor: {messageText}");
        }
    }
}
