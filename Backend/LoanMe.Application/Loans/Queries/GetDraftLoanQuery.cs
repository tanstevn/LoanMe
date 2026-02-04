using LoanMe.Data;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;

namespace LoanMe.Application.Loans.Queries {
    public class GetDraftLoanQuery : IQuery<Result<GetDraftLoanQueryResult>> {
        public long Id { get; set; }
    }

    public class GetDraftLoanQueryResult {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public short Term { get; set; }
        public decimal LoanAmount { get; set; }
    }

    public class GetDraftLoanQueryHandler : IRequestHandler<GetDraftLoanQuery, Result<GetDraftLoanQueryResult>> {
        private readonly ApplicationDbContext _dbContext;

        public GetDraftLoanQueryHandler(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public virtual async Task<Result<GetDraftLoanQueryResult>> HandleAsync(GetDraftLoanQuery request) {
            var draftLoan = await _dbContext.DraftLoans
                .FindAsync(request.Id);

            if (draftLoan is null) {
                throw new Exception($"There is no existing draft loan with id of: {request.Id}");
            }

            return Result<GetDraftLoanQueryResult>
                .Success(new() {
                    Title = draftLoan.User.Title,
                    FirstName = draftLoan.User.FirstName,
                    LastName = draftLoan.User.LastName,
                    DateOfBirth = draftLoan.User.DateOfBirth,
                    MobileNumber = draftLoan.User.MobileNumber,
                    EmailAddress = draftLoan.User.EmailAddress,
                    Term = draftLoan.Term,
                    LoanAmount = draftLoan.LoanAmount
                });
        }
    }
}
