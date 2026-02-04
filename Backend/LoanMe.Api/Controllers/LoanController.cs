using LoanMe.Application.Loans.Commands;
using LoanMe.Application.Loans.Queries;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoanMe.Api.Controllers {
    [Route("api/loan")]
    public class LoanController : BaseController {
        private readonly IMediator _mediator;

        public LoanController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet("draft")]
        public async Task<Result<GetDraftLoanQueryResult>> GetDraftLoan([FromQuery] GetDraftLoanQuery query) {
            return await _mediator.SendAsync(query);
        }

        [HttpPost("draft")]
        public async Task<IActionResult> DraftLoan([FromBody] DraftLoanCommand command) {
            var result = await _mediator.SendAsync(command);
            return Redirect(result.Data?.RedirectURL!);
        }

        [HttpPut("draft/update")]
        public async Task<Result<UpdateDraftLoanCommandResult>> UpdateDraftLoan([FromBody] UpdateDraftLoanCommand command) {
            return await _mediator.SendAsync(command);
        }

        [HttpPost]
        public async Task<Result<ApplyLoanCommandResult>> ApplyLoan([FromBody] ApplyLoanCommand command) {
            return await _mediator.SendAsync(command);
        }

        [HttpGet("calculator/quote")]
        public async Task<Result<CalculateQuoteQueryResult>> GetCalculatedQuote([FromQuery] CalculateQuoteQuery query) {
            return await _mediator.SendAsync(query);
        }
    }
}
