using Engie.Infrastructure.Models;
using MediatR;

namespace Engie.Application.ProductionPlan
{
    internal sealed class ProductionRequestHandler : IRequestHandler<ProductionRequest, IReadOnlyCollection<ProductionResponse>>
    {
        public Task<IReadOnlyCollection<ProductionResponse>> Handle(ProductionRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
