using System.Text.Json.Serialization;

namespace Engie.Contract
{

    public sealed record ProductionPlanResponse(
   [property: JsonPropertyName("name")] string Name,
   [property: JsonPropertyName("p")] double Production);
}