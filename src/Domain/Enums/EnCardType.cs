using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CardActionService.Domain.Enums;

[DataContract]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EnCardType
{
    
    [EnumMember(Value = "Unknown")]
    Unknown = 0,

    [EnumMember(Value = "Debit Card")]
    Debit = 1,

    [EnumMember(Value = "Credit Card")]
    Credit = 2,

    [EnumMember(Value = "Prepaid Card")]
    Prepaid = 3
}