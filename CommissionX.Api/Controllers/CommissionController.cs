using CommissionX.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommissionX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommissionController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateCommission([FromBody] CreateCommisionByInvoiceCommand command)
        {
            var results = await _mediator.Send(command);

            return Ok(results);
        }
    }
}