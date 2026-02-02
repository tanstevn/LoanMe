using LoanMe.Application.Loan.Commands;
using LoanMe.Application.Loan.Queries;
using Microsoft.AspNetCore.Mvc;

namespace LoanMe.Api.Controllers {
    [Route("api/loan")]
    [ApiController, Produces("application/json")]
    public class LoanController : ControllerBase {

        [HttpGet]
        public async Task GetLoan([FromQuery] GetLoanQuery query) {

        }

        [HttpPost("draft")]
        public async Task<IActionResult> DraftLoan([FromBody] DraftLoanCommand command) {
            return Redirect(default!);
        }

        [HttpPut("draft/update")]
        public async Task<IActionResult> UpdateDraftLoan([FromBody] UpdateDraftLoanCommand command) {
            return Redirect(default!);
        }

        [HttpPost]
        public async Task ApplyLoan([FromBody] ApplyLoanCommand command) {

        }
    }
}
