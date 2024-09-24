using MassTransit;
using CommunWork.Mensajes; // Ensure correct namespace for MyMessage
using SubscriptoAplicacion.Data; // Import the DbContext
using SubscriptoAplicacion.Models; // Import the MessageModel
using System.Threading.Tasks;

namespace SubscriptoAplicacion.Consumers
{
    public class MyConsumer : IConsumer<MyMessage>
    {
        private readonly ApplicationDbContext _dbContext;

        // Injecting the database context in the constructor
        public MyConsumer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Consume(ConsumeContext<MyMessage> context)
        {
            string topic = context.Message.Topic;
            string messageText = context.Message.Text;

            // Log the received message
            Console.WriteLine($"Mensaje recibido: {messageText}");
            Console.WriteLine($"Tópico: {topic}");

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
                await _dbContext.SaveChangesAsync(); // Save to the database
                Console.WriteLine("Mensaje guardado en la base de datos exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el mensaje en la base de datos: {ex.Message}");
            }
        }
    }
}