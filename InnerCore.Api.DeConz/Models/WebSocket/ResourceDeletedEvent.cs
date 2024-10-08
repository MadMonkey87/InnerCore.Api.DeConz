using System;

namespace InnerCore.Api.DeConz.Models.WebSocket
{
	public class ResourceDeletedEvent : EventArgs
	{
		public string Id { get; set; }

		public ResourceType ResourceType { get; set; }
	}
}
