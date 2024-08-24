using System;

namespace InnerCore.Api.DeConz.Models.WebSocket
{
	public class GroupChangedEvent : EventArgs
	{
		public string Id { get; set; }

		public LightState State { get; set; }
	}
}
