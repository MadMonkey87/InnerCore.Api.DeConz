using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Schedule
{
    public enum ScheduleStatus
    {
        [EnumMember(Value = "enabled")]
        Enabled,
        [EnumMember(Value = "disabled")]
        Disabled,
    }
}
