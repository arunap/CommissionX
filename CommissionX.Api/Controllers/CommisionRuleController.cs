using CommissionX.Application.Commands;
using CommissionX.Application.Queries;
using CommissionX.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommissionX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommisionRuleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommisionRuleController(IMediator mediator) => _mediator = mediator;

        // GET: api/CommissionRule/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCommissionRuleById(Guid id)
        {
            var commissionRule = await _mediator.Send(new GetCommissionRuleByIdQuery(id));

            if (commissionRule == null)
            {
                return NotFound();
            }

            return Ok(commissionRule);
        }

        // GET: api/CommissionRule/type/{commissionRuleType}
        [HttpGet("type/{commissionRuleType}")]
        public async Task<IActionResult> GetCommissionRulesByCommissionRuleType(CommissionRuleType commissionRuleType)
        {
            var commissionRules = await _mediator.Send(new GetCommissionRulesByTypeQuery(commissionRuleType));

            if (commissionRules == null || commissionRules.Count == 0)
            {
                return NotFound();
            }

            return Ok(commissionRules);
        }


        // POST: api/CommissionRule
        [HttpPost]
        public async Task<IActionResult> CreateCommissionRule([FromBody] CreateCommissionRuleCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid commission rule data.");
            }

            var commissionRuleId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCommissionRuleById), new { id = commissionRuleId }, commissionRuleId);
        }

        // PUT: api/CommissionRule/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCommissionRule(Guid id, [FromBody] UpdateCommissionRuleCommand command)
        {
            if (command == null || id != command.Id)
            {
                return BadRequest("Invalid commission rule data or mismatched ID.");
            }

            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/CommissionRule/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCommissionRule(Guid id)
        {
            await _mediator.Send(new DeleteCommissionRuleCommand(id));
            return NoContent();
        }
    }
}