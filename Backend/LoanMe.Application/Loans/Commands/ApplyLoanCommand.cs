using LoanMe.Data;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;

namespace LoanMe.Application.Loans.Commands {
    public class ApplyLoanCommand : ICommand<Result<ApplyLoanCommandResult>> {
        public long Id { get; set; }
        public decimal RepaymentAmount { get; set; }
        public decimal EstablishmentFee { get; set; }
        public decimal TotalInterest { get; set; }
    }

    public class ApplyLoanCommandResult {
        public string? ApplicationNumber { get; set; }
    }

    public class ApplyLoanCommandHandler : IRequestHandler<ApplyLoanCommand, Result<ApplyLoanCommandResult>> {
        private readonly ApplicationDbContext _dbContext;

        public ApplyLoanCommandHandler(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Result<ApplyLoanCommandResult>> HandleAsync(ApplyLoanCommand request) {
            return Result<ApplyLoanCommandResult>
                .Success(new());
        }
    }
}
