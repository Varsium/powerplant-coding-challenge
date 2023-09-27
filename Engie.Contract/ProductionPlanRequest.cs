using Engie.Infrastructure.Models;
using System.Text.Json.Serialization;

namespace Engie.Contract
{
    public sealed record ProductionPlanRequest(
         [property: JsonPropertyName("load")] int Load,
         [property: JsonPropertyName("fuels")] Fuel Fuels,
         [property: JsonPropertyName("powerplants")] IReadOnlyList<Powerplant> Powerplants
     );
}
