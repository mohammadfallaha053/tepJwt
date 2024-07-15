using System.Text.Json.Serialization;

namespace JWT53.Enum.Property
{
   // [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum Status
    {
        Available,
        Unavailable
    }
}
