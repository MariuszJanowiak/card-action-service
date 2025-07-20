using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CardActionService.Domain.Enums;

[DataContract]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EnCardType
{
    [EnumMember(Value = "Debit Card")]
    Debit = 0,

    [EnumMember(Value = "Credit Card")]
    Credit = 1,

    [EnumMember(Value = "Prepaid Card")]
    Prepaid = 2
}
