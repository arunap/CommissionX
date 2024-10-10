using CommissionX.Application.Queries;
using CommissionX.Core.Entities.Comissions;
using CommissionX.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommissionX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommisionRulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommisionRulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CommissionRuleTypes ruleType)
        {
            switch (ruleType)
            {
                case CommissionRuleTypes.FLAT:
                    var flats = await _mediator.Send(new GetCommissionRulesByTypeQuery<FlatCommissionRule>() { CommissionRuleType = CommissionRuleTypes.FLAT });
                    return Ok(flats);

                case CommissionRuleTypes.PERCENTAGE:
                    var pecents = await _mediator.Send(new GetCommissionRulesByTypeQuery<PercentageCommisionRule>() { CommissionRuleType = CommissionRuleTypes.PERCENTAGE });
                    return Ok(pecents);

                case CommissionRuleTypes.TIERED:
                    var tiered = await _mediator.Send(new GetCommissionRulesByTypeQuery<TireCommisionRule>() { CommissionRuleType = CommissionRuleTypes.TIERED });
                    return Ok(tiered);

                case CommissionRuleTypes.CAP:
                    var caps = await _mediator.Send(new GetCommissionRulesByTypeQuery<CapCommissionRule>() { CommissionRuleType = CommissionRuleTypes.CAP });
                    return Ok(caps);

                default:
                    return BadRequest("Invalid Rule Type");
            }
        }
    }
}