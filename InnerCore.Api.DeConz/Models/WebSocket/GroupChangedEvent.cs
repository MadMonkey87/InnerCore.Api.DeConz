using InnerCore.Api.DeConz.Models.Groups;
using System;

namespace InnerCore.Api.DeConz.Models.WebSocket
{
	public class GroupChangedEvent : EventArgs
	{
		public string Id { get; set; }

		public GroupState State { get; set; }
	}
}
