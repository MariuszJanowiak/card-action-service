using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CardActionService.Domain.Enums;

[DataContract]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EnCardStatus
{
    [EnumMember(Value = "Ordered")]
    Ordered = 0,

    [EnumMember(Value = "Inactive")]
    Inactive = 1,

    [EnumMember(Value = "Active")]
    Active = 2,

    [EnumMember(Value = "Restricted")]
    Restricted = 3,

    [EnumMember(Value = "Blocked")]
    Blocked = 4,

    [EnumMember(Value = "Expired")]
    Expired = 5,

    [EnumMember(Value = "Closed")]
    Closed = 6
}
