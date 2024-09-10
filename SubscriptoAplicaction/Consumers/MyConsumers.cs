// SubscriptoAplicacion/Consumers/MyConsumer.cs
using MassTransit;
using CommunWork.Mensajes; // Asegúrate de importar el namespace correcto del mensaje
using System.Threading.Tasks;

namespace SubscriptoAplicacion.Consumers
{
    public class MyConsumer : IConsumer<MyMessage>
    {
        public Task Consume(ConsumeContext<MyMessage> context)
        {
            // Lógica para procesar el mensaje recibido
            Console.WriteLine($"Mensaje recibido: {context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}
