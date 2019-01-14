using InnerCore.Api.DeConz.Models.WebSocket;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InnerCore.Api.DeConz
{
    /// <summary>
    /// Partial DeConzClient, contains websocket management
    /// </summary>
    public partial class DeConzClient
    {
        public event EventHandler<SensorChangedEvent> SensorChanged;

        public event EventHandler<ErrorEvent> ErrorEvent;

        public async Task ListenToEvents()
        {
            CheckInitialized();

            var config = await GetConfigAsync();

            await ListenToEvents(_ip, config.WebsocketPort, CancellationToken.None);
        }

        public async Task ListenToEvents(CancellationToken cancellationToken)
        {
            CheckInitialized();

            var config = await GetConfigAsync();

            await ListenToEvents(_ip, config.WebsocketPort, cancellationToken);
        }

        public async Task ListenToEvents(string ip, int port)
        {
            await ListenToEvents(ip, port, CancellationToken.None);
        }

        public async Task ListenToEvents(string ip, int port, CancellationToken cancellationToken)
        {
            Uri uri;
            if (!Uri.TryCreate(string.Format("ws://{0}:{1}", ip, port), UriKind.Absolute, out uri))
            {
                //Invalid ip or hostname caused Uri creation to fail
                throw new Exception(string.Format("The supplied ip to the DeConzClient is not a valid ip: {0}:{1}", ip, port));
            }

            using (var webSocket = new ClientWebSocket())
            {
                await webSocket.ConnectAsync(uri, cancellationToken);

                while (webSocket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var buffer = WebSocket.CreateClientBuffer(Constants.WEB_SOCKET_BUFFER_SIZE, 1);
                        var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                        }
                        if (result.MessageType != WebSocketMessageType.Text)
                        {
                            throw new NotSupportedException($"unsupported message type: {result.MessageType}");
                        }
                        else if (!result.EndOfMessage)
                        {
                            throw new NotSupportedException($"the message appears to be sent in chunks which is not supported");
                        }
                        else
                        {
                            HandleResult(Encoding.UTF8.GetString(buffer.Array).TrimEnd((char)0));
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorEvent?.Invoke(this, new ErrorEvent(ex));
                    }
                }
            }
        }

        private void HandleResult(string result)
        {
            if (SensorChanged != null)
            {
                var message = JsonConvert.DeserializeObject<Message>(result);

                if (message.Event == EventType.Changed && message.Type == MessageType.Event && message.ResourceType == ResourceType.Sensor)
                {
                    SensorChanged(this, new SensorChangedEvent()
                    {
                        Id = message.Id,
                        Config = message.Config,
                        State = message.State
                    });
                }
                else
                {
                    throw new NotSupportedException($"not supported message (event: {message.Event}, type: {message.Type}, resource: {message.ResourceType})");
                }
            }
        }
    }
}
