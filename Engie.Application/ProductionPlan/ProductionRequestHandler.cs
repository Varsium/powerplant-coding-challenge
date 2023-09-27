using Engie.Infrastructure.Enums;
using Engie.Infrastructure.Models;
using MediatR;

namespace Engie.Application.ProductionPlan
{
    internal sealed class ProductionRequestHandler : IRequestHandler<ProductionRequest, List<ProductionResponse>>
    {
        public Task<List<ProductionResponse>> Handle(ProductionRequest request, CancellationToken cancellationToken)
        {
            // Sort the power plants by cost per unit of electricity (merit-order)
            //CO2 feature a bit unclear so far. 
            var updatedPowerPlants = request.Powerplants.Select(powerPlant =>
            {
                var efficiencyMultiplier = powerPlant.Type switch
                {
                    EResourceType.GasFired => request.Fuels.GasEuroMWh,
                    EResourceType.TurboJet => request.Fuels.KerosineEuroMWh,
                    _ => 0, // Wind is always free

                };
                return new Powerplant(powerPlant.Name, powerPlant.Type, powerPlant.Efficiency / efficiencyMultiplier, powerPlant.Pmin, powerPlant.Pmax);

            }).ToList();
            var sortedPowerPlants = updatedPowerPlants.OrderByDescending(pp => pp.Efficiency).ToList();
            var totalPowerProduced = 0.0;
            var activatedPowerPlants = new List<ProductionResponse>();
            for (int i = 0; i < sortedPowerPlants.Count; i++)
            {
                var powerPlant = sortedPowerPlants[i];

                if (request.Fuels.Wind == 0 && powerPlant.Type == EResourceType.WindTurbine)
                {
                    activatedPowerPlants.Add(new(powerPlant.Name, 0));
                    continue;
                }

                var remainingLoad = request.Load - totalPowerProduced;
                var powerToProduce = Math.Min(powerPlant.Pmax, Math.Max(powerPlant.Pmin, remainingLoad));

                if (powerPlant.Type == EResourceType.WindTurbine)
                {
                    powerToProduce *= (request.Fuels.Wind / 100); // Always adjust to the % available wind
                }
                if (i + 1 < sortedPowerPlants.Count && sortedPowerPlants[i + 1].Pmin + totalPowerProduced + powerToProduce > request.Load)
                {
                    powerToProduce -= sortedPowerPlants[i + 1].Pmin;
                }
                powerToProduce = Math.Round(powerToProduce, 1); // Round the numbers up

                if (totalPowerProduced + powerToProduce <= request.Load)
                {
                    activatedPowerPlants.Add(new(powerPlant.Name, powerToProduce));
                    totalPowerProduced += powerToProduce;
                    continue;
                }

                activatedPowerPlants.Add(new(powerPlant.Name, 0));
            }

            if (Math.Abs(totalPowerProduced - request.Load) > 0.01)
            {
                throw new Exception("The power produced is not the same as the load");

            }

            return Task.FromResult(activatedPowerPlants);
        }
    }
}
