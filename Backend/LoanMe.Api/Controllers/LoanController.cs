using LoanMe.Application.Loan.Commands;
using LoanMe.Application.Loan.Queries;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoanMe.Api.Controllers {
    [Route("api/loan")]
    [ApiController, Produces("application/json")]
    public class LoanController : ControllerBase {
        private readonly IMediator _mediator;

        public LoanController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Result<GetLoanQueryResult>> GetLoan([FromQuery] GetLoanQuery query) {
            return await _mediator.SendAsync(query);
        }

        [HttpPost("draft")]
        public async Task<IActionResult> DraftLoan([FromBody] DraftLoanCommand command) {
            var result = await _mediator.SendAsync(command);
            return Redirect(result.Data?.RedirectURL!);
        }

        [HttpPut("draft/update")]
        public async Task<IActionResult> UpdateDraftLoan([FromBody] UpdateDraftLoanCommand command) {
            var result = await _mediator.SendAsync(command);
            return Redirect(result.Data?.RedirectURL!);
        }

        [HttpPost]
        public async Task<Result<ApplyLoanCommandResult>> ApplyLoan([FromBody] ApplyLoanCommand command) {
            return await _mediator.SendAsync(command);
        }
    }
}
