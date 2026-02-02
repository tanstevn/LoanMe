using LoanMe.Data;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;

namespace LoanMe.Application.Loan.Commands {
    public class UpdateDraftLoanCommand : ICommand<Result<UpdateDraftLoanCommandResult>> {
        public long Id { get; set; }
        public DraftLoanCommand? Data { get; set; }
    }

    public class UpdateDraftLoanCommandResult {
        public string? RedirectURL { get; set; }
    }

    public class UpdateDraftLoanCommandHandler : IRequestHandler<UpdateDraftLoanCommand, Result<UpdateDraftLoanCommandResult>> {
        private readonly ApplicationDbContext _dbContext;

        public UpdateDraftLoanCommandHandler(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Result<UpdateDraftLoanCommandResult>> HandleAsync(UpdateDraftLoanCommand request) {
            return Result<UpdateDraftLoanCommandResult>
                .Success(new());
        }
    }
}
