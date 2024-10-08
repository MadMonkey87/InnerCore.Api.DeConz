using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.WebSocket
{
    /// <summary>
    /// Specifies the type of an event message
    /// </summary>
    public enum EventType
    {
        [EnumMember(Value = "changed")]
        Changed,

		[EnumMember(Value = "added")]
		Added,

		[EnumMember(Value = "deleted")]
		Deleted,

		[EnumMember(Value = "scene-called")]
		SceneRecalled
	}
}
