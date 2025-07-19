using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CardActionService.Domain.Enums;

[DataContract]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EnCardStatus
{
    [EnumMember(Value = "Inactive")]
    Inactive = 0,

    [EnumMember(Value = "Active")]
    Active = 1,

    [EnumMember(Value = "Blocked")]
    Blocked = 2,

    [EnumMember(Value = "Expired")]
    Expired = 3
}