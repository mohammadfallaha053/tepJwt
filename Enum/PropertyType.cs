namespace JWT53.Enum;

using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PropertyType
{
    Rent,
    Sale
}
