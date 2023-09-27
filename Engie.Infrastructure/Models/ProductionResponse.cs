using System.Text.Json.Serialization;

namespace Engie.Infrastructure.Models
{
    public sealed record ProductionResponse
    (
        [property: JsonPropertyName("name")]
        string Name,
  [property: JsonPropertyName("p")] double Production);
  
}
