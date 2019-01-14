using System.Runtime.Serialization;

namespace InnerCore.Api.deConz.Models.Scenes
{
    public enum SceneType
    {
        [EnumMember(Value = "LightScene")]
        LightScene,
        [EnumMember(Value = "GroupScene")]
        GroupScene,
    }
}