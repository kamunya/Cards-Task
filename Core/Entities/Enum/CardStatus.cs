using System.Runtime.Serialization;

namespace Cards.Core.Entities.Enum
{
    public enum CardStatus
    {
        [EnumMember(Value = "To do")]
        ToDo,
        [EnumMember(Value = "In Progress")]
        InProgress,
        [EnumMember(Value = "Done")]
        Done

    }
}
