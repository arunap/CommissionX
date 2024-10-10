using CommissionX.Application.DTOs;
using CommissionX.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommissionX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] InvoiceDto invoice)
        {
            var results = await _mediator.Send(new GetCommisionByInvoiceQuery { Invoice = invoice });

            return Ok(results);
        }
    }
}