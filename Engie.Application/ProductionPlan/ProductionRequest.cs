using Engie.Infrastructure.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace Engie.Application.ProductionPlan
{
    public sealed record ProductionRequest
      (
         [property: JsonPropertyName("load")] int Load,
         [property: JsonPropertyName("fuels")] Fuel Fuels,
         [property: JsonPropertyName("powerplants")] IReadOnlyList<Powerplant> Powerplants
     ) : IRequest<List<ProductionResponse>>;
}
