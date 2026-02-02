using LoanMe.Data;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;

namespace LoanMe.Application.Loan.Queries {
    public class GetLoanQuery : IQuery<Result<GetLoanQueryResult>> {
        public long Id { get; set; }
    }

    public class GetLoanQueryResult {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal LoanAmount { get; set; }
    }

    public class GetLoanQueryHandler : IRequestHandler<GetLoanQuery, Result<GetLoanQueryResult>> {
        private readonly ApplicationDbContext _dbContext;

        public GetLoanQueryHandler(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Result<GetLoanQueryResult>> HandleAsync(GetLoanQuery request) {

            return Result<GetLoanQueryResult>
                .Success(new());
        }
    }
}
