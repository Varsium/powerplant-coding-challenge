using Engie.Infrastructure.Enums;
using FluentValidation;
using FluentValidation.Results;

namespace Engie.Application.ProductionPlan
{
    internal class ProductionRequestValidator : AbstractValidator<ProductionRequest>
    {
        public ProductionRequestValidator()
        {
            RuleFor(productionRequest => productionRequest.Load).NotEmpty();
            RuleFor(productionRequest => productionRequest.Fuels)
                .ChildRules(fuel =>
                {
                    fuel.RuleFor(f => f.KerosineEuroMWh).NotEmpty();
                    fuel.RuleFor(f => f.Co2EuroTon).NotEmpty();
                    fuel.RuleFor(f => f.GasEuroMWh).NotEmpty();
                    fuel.RuleFor(f => f.Wind).NotNull();
                }
                );
            RuleFor(productionRequest => productionRequest.Powerplants).NotEmpty();

            RuleFor(productionRequest => productionRequest.Powerplants)
                .ForEach(powerplant =>
                {
                    powerplant.ChildRules(powerp =>
                    {
                        powerp.RuleFor(power => power.Name).NotEmpty();
                        powerp.RuleFor(power => power.Efficiency).NotNull(); //Can efficiency drop to 0?
                       
                    });
                });
        }

        protected override bool PreValidate(ValidationContext<ProductionRequest> context, ValidationResult result)
        {
            var boolList = new List<bool>();
            foreach (var powerplant in context.InstanceToValidate.Powerplants)
            {
                var enumType = typeof(EResourceType);
                var ignoreCase = true;

                if (powerplant.Type.GetType() != enumType)
                {
                    context.MessageFormatter.AppendArgument("EnumType", enumType.Name);
                    boolList.Add(false);
                }

                if (!Enum.IsDefined(enumType, powerplant.Type))
                {
                    context.MessageFormatter.AppendArgument("EnumType", enumType.Name);
                    boolList.Add(false);
                }

                if (!Enum.TryParse(enumType, powerplant.Type.ToString(), ignoreCase, out var EnumResult))
                {
                    context.MessageFormatter.AppendArgument("EnumType", enumType.Name);
                    boolList.Add(false);
                }
            }
            if (boolList.Any())
            {
                throw new ValidationException("A powerplant contains an Unmatched Type");
            }

            return !boolList.Any();

        }

    }
}
