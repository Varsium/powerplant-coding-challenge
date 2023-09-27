using System.Text.Json.Serialization;

namespace Engie.Infrastructure.Models
{
    public sealed record Fuel(
         [property: JsonPropertyName("gas(euro/MWh)")] double GasEuroMWh,
         [property: JsonPropertyName("kerosine(euro/MWh)")] double KerosineEuroMWh,
         [property: JsonPropertyName("co2(euro/ton)")] double Co2EuroTon,
         [property: JsonPropertyName("wind(%)")] double Wind
     );
}
