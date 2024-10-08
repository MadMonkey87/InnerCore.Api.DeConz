using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InnerCore.Api.DeConz.Models;
using InnerCore.Api.DeConz.Models.Groups;
using InnerCore.Api.DeConz.Models.Sensors;
using InnerCore.Api.DeConz.Models.WebSocket;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz
{
	/// <summary>
	/// Partial DeConzClient, contains websocket management
	/// </summary>
	public partial class DeConzClient
	{
		public event EventHandler<SensorChangedEvent> SensorChanged;

		public event EventHandler<LightChangedEvent> LightChanged;

		public event EventHandler<GroupChangedEvent> GroupChanged;

		public event EventHandler<ResourceAddedEvent> ResourceAdded;

		public event EventHandler<ResourceDeletedEvent> ResourceDeleted;

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
				throw new Exception(
					string.Format(
						"The supplied ip to the DeConzClient is not a valid ip: {0}:{1}",
						ip,
						port
					)
				);
			}

			using (var webSocket = new ClientWebSocket())
			{
				await webSocket.ConnectAsync(uri, cancellationToken);

				while (webSocket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
				{
					try
					{
						var buffer = WebSocket.CreateClientBuffer(
							Constants.WEB_SOCKET_BUFFER_SIZE,
							1
						);

						var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

						if (result.MessageType == WebSocketMessageType.Close)
						{
							await webSocket.CloseAsync(
								WebSocketCloseStatus.NormalClosure,
								string.Empty,
								CancellationToken.None
							);
						}
						if (result.MessageType != WebSocketMessageType.Text)
						{
							throw new NotSupportedException(
								$"unsupported message type: {result.MessageType}"
							);
						}
						else if (!result.EndOfMessage)
						{
							throw new NotSupportedException($"the message appears to be sent in chunks which is not supported"
							);
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
			var message = JsonConvert.DeserializeObject<Message>(result);

			switch (message.Event)
			{
				case EventType.Changed:
					switch (message.ResourceType)
					{
						case ResourceType.Sensor:
							if (SensorChanged != null)
							{
								SensorChanged(
									this,
									new SensorChangedEvent()
									{
										Id = message.Id,
										Config = message.SensorConfig,
										State =
											message.State != null
												? message.State.ToObject<SensorState>()
												: null,
									}
								);
							}
							break;
						case ResourceType.Light:
							if (LightChanged != null)
							{
								LightChanged(
									this,
									new LightChangedEvent()
									{
										Id = message.Id,
										State =
											message.State != null ? message.State.ToObject<LightState>() : null,
									}
								);
							}
							break;
						case ResourceType.Group:
							if (GroupChanged != null)
							{
								GroupChanged(
									this,
									new GroupChangedEvent()
									{
										Id = message.Id,
										State =
											message.State != null ? message.State.ToObject<GroupState>() : null,
									}
								);
							}
							break;
					}
					break;
				case EventType.Added:
					if (ResourceAdded != null)
					{
						ResourceAdded(this,
							new ResourceAddedEvent()
							{
								Id = message.Id,
								ResourceType = message.ResourceType
							}
						);
					}
					break;
				case EventType.Deleted:
					if (ResourceDeleted != null)
					{
						ResourceDeleted(this,
							new ResourceDeletedEvent()
							{
								Id = message.Id,
								ResourceType = message.ResourceType
							}
						);
					}
					break;
			}


		}
	}
}
