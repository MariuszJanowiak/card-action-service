using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CardActionService.Domain.Enums;

[DataContract]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EnCardStatus
{
    [EnumMember(Value = "Ordered")]
    Ordered,

    [EnumMember(Value = "Inactive")]
    Inactive,

    [EnumMember(Value = "Active")]
    Active,

    [EnumMember(Value = "Restricted")]
    Restricted,

    [EnumMember(Value = "Blocked")]
    Blocked,

    [EnumMember(Value = "Expired")]
    Expired,

    [EnumMember(Value = "Closed")]
    Closed
}