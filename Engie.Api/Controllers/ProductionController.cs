using Engie.Application.ProductionPlan;
using Engie.Contract;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace coding_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductionController : ControllerBase
    {

        private readonly ISender _sender;
        private readonly ILogger<ProductionController> _logger;

        public ProductionController(ILogger<ProductionController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [ProducesResponseType(typeof(ProductionPlanResponse), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [HttpPost(Name = nameof(ProductionPLanAsync))]
        public async Task<IEnumerable<ProductionPlanResponse>> ProductionPLanAsync(ProductionPlanRequest request)
        {
            var productionRequest = new ProductionRequest(request.Load, request.Fuels, request.Powerplants);
            var result = await _sender.Send(productionRequest);
            return result.Select(pr => new ProductionPlanResponse(pr.Name, pr.Production));
        }
    }
}