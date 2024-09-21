using MassTransit;
using CommunWork.Mensajes; // Asegúrate de importar el namespace correcto del mensaje
using SubscriptoAplicacion.Data; // Importa el contexto de base de datos
using SubscriptoAplicacion.Models; // Importa el modelo
using System.Threading.Tasks;

namespace SubscriptoAplicacion.Consumers
{
    public class MyConsumer : IConsumer<MyMessage>
    {
        private readonly WebSocketConnectionManager _connectionManager;
        private readonly ApplicationDbContext _dbContext; // Inyecta el contexto de la base de datos

        public MyConsumer(WebSocketConnectionManager connectionManager, ApplicationDbContext dbContext)
        {
            _connectionManager = connectionManager;
            _dbContext = dbContext; // Asigna el contexto de base de datos
        }

        public async Task Consume(ConsumeContext<MyMessage> context)
        {
            // Lógica para procesar el mensaje recibido
            string topic = context.Message.Topic;
            string messageText = context.Message.Text;

            // Imprimir en consola el mensaje recibido
            Console.WriteLine($"Mensaje recibido: {messageText}");
            Console.WriteLine($"Tópico: {topic}");

            // Intentar guardar el mensaje en la base de datos
            var message = new MessageModel
            {
                Texto = messageText,
                Topico = topic,
                Fecha = DateTime.Now
            };

            try
            {
                Console.WriteLine("Intentando guardar el mensaje en la base de datos...");
                _dbContext.Messages.Add(message);
                await _dbContext.SaveChangesAsync(); // Guardar en la base de datos
                Console.WriteLine("Mensaje guardado en la base de datos exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el mensaje en la base de datos: {ex.Message}");
            }

            // Lógica para enviar solo a los WebSockets suscritos al tópico correspondiente
            await _connectionManager.SendMessageToTopicAsync(topic, $"Mensaje recibido: {messageText}");
        }
    }
}
