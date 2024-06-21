using System.Text.Json.Serialization;

namespace Ebanx.Enumerations;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TypeEnum
{
    Deposit = 1,
    Withdraw = 2,
    Transfer = 3
}
