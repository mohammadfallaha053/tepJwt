using System.Text.Json.Serialization;

namespace JWT53.Enum.Seller
{
  //  [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum Status
    {
        Waiting,
        Rejected,
        Accepted
    }
}
