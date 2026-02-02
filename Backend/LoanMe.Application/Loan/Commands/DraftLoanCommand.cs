using LoanMe.Data;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;

namespace LoanMe.Application.Loan.Commands {
    public class DraftLoanCommand : ICommand<Result<DraftLoanCommandResult>> {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public short Term { get; set; }
        public decimal LoanAmount { get; set; }
    }

    public class DraftLoanCommandResult {
        public string? RedirectURL { get; set; }
    }

    public class DraftLoanCommandHandler : IRequestHandler<DraftLoanCommand, Result<DraftLoanCommandResult>> {
        private readonly ApplicationDbContext _dbContext;

        public DraftLoanCommandHandler(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Result<DraftLoanCommandResult>> HandleAsync(DraftLoanCommand request) {
            return Result<DraftLoanCommandResult>
                .Success(new());
        }
    }
}
