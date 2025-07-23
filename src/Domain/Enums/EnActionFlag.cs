using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CardActionService.Domain.Enums;

[DataContract]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EnActionFlag
{
    [EnumMember(Value = "N")]
    No = 0,

    [EnumMember(Value = "Y")]
    Yes = 1,

    [EnumMember(Value = "U")]
    UnlessNoPin = 2,

    [EnumMember(Value = "P")]
    IfPinSet = 3,

    [EnumMember(Value = "Q")]
    IfPinNotSet = 4
}
