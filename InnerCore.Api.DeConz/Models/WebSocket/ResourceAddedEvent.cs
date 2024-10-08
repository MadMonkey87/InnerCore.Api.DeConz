using System;

namespace InnerCore.Api.DeConz.Models.WebSocket
{
	public class ResourceAddedEvent : EventArgs
	{
		public string Id { get; set; }

		public ResourceType ResourceType { get; set; }
	}
}
