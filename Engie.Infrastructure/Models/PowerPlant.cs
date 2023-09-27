using Engie.Infrastructure.Enums;
using System.Text.Json.Serialization;

namespace Engie.Infrastructure.Models
{
    public sealed record Powerplant(
      [property: JsonPropertyName("name")] string Name,
      [property: JsonPropertyName("type")][property: JsonConverter(typeof(JsonStringEnumConverter))] EResourceType Type,
      [property: JsonPropertyName("efficiency")] double Efficiency,
      [property: JsonPropertyName("pmin")] int Pmin,
      [property: JsonPropertyName("pmax")]  int Pmax
  );
}
