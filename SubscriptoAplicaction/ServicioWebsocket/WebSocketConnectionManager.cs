using System.Net.WebSockets;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubscriptoAplicacion.Services
{
    public class WebSocketConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        // Agregar un nuevo WebSocket al diccionario
        public string AddSocket(WebSocket socket)
        {
            string connectionId = Guid.NewGuid().ToString();
            _sockets.TryAdd(connectionId, socket);
            return connectionId;
        }

        // Remover un WebSocket del diccionario
        public async Task RemoveSocketAsync(string connectionId)
        {
            if (_sockets.TryRemove(connectionId, out WebSocket socket))
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
                socket.Dispose();
            }
        }

        // Enviar un mensaje a todos los WebSockets conectados
        public async Task SendMessageToAllAsync(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            foreach (var socket in _sockets.Values)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
