using System.Text.Json.Serialization;

namespace Engie.Infrastructure.Models
{
    public record Powerplant(
      [property: JsonPropertyName("name")] string Name,
      [property: JsonPropertyName("type")] string Type,
      [property: JsonPropertyName("efficiency")] double Efficiency,
      [property: JsonPropertyName("pmin")] int Pmin,
      [property: JsonPropertyName("pmax")] int Pmax
  );
}
