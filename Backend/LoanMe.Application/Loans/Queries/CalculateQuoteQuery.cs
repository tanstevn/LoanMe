using LoanMe.Data;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;

namespace LoanMe.Application.Loans.Queries {
    public class CalculateQuoteQuery : IQuery<Result<CalculateQuoteQueryResult>> {
        public long ProductId { get; set; }
        public long DraftLoanId { get; set; }
    }

    public class CalculateQuoteQueryResult {
        public string? FullName { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public decimal LoanAmount { get; set; }
        public short LoanTerm { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal EstablishmentFee { get; set; }
        public decimal RepaymentAmount { get; set; }
        public bool IsApplied { get; set; }
    }

    public class CalculateQuoteQueryHandler : IRequestHandler<CalculateQuoteQuery, Result<CalculateQuoteQueryResult>> {
        private readonly ApplicationDbContext _dbContext;

        public CalculateQuoteQueryHandler(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public virtual async Task<Result<CalculateQuoteQueryResult>> HandleAsync(CalculateQuoteQuery request) {
            var draftLoan = await _dbContext.DraftLoans
                .FindAsync(request.DraftLoanId);

            if (draftLoan is null) {
                throw new Exception($"There is no existing draft loan with id of: {request.DraftLoanId}");
            }

            var product = await _dbContext.Products
                .FindAsync(request.ProductId);

            if (product is null) {
                throw new Exception($"There is no existing product with id of: {request.ProductId}");
            }

            var fullName = string.Format("{0} {1}", draftLoan.User.FirstName,
                draftLoan.User.LastName);

            var principalAmount = draftLoan.LoanAmount + product.EstablishmentFee;

            if (product.InterestRate == default) {
                return Result<CalculateQuoteQueryResult>
                    .Success(new() {
                        FullName = fullName,
                        MobileNumber = draftLoan.User.MobileNumber,
                        EmailAddress = draftLoan.User.EmailAddress,
                        LoanAmount = draftLoan.LoanAmount,
                        LoanTerm = draftLoan.Term,
                        TotalInterest = default,
                        EstablishmentFee = product.EstablishmentFee,
                        RepaymentAmount = principalAmount / draftLoan.Term,
                        IsApplied = draftLoan.IsApplied
                    });
            }

            var monthlyRate = product.InterestRate / 12;

            var r = (double)monthlyRate;
            var pv = (double)principalAmount;
            var n = draftLoan.Term;

            var repaymentAmount = default(decimal);
            var paidAmount = default(decimal);
            var totalInterest = default(decimal);

            if (product.InterestFreeMonths is not null and not 0) {
                var freeMonths = product.InterestFreeMonths.Value;
                var effectiveTerm = n - freeMonths;
                var freeRepayments = (principalAmount / n) * freeMonths;

                repaymentAmount = (decimal)(r * pv / (1 - Math.Pow(1 + r, -effectiveTerm)));
                paidAmount = freeRepayments + (repaymentAmount * effectiveTerm);
                totalInterest = paidAmount - principalAmount;
            }
            else {
                repaymentAmount = (decimal)(r * pv / (1 - Math.Pow(1 + r, -n)));
                paidAmount = repaymentAmount * draftLoan.Term;
                totalInterest = paidAmount - principalAmount;
            }

            return Result<CalculateQuoteQueryResult>
                .Success(new() {
                    FullName = fullName,
                    MobileNumber = draftLoan.User.MobileNumber,
                    EmailAddress = draftLoan.User.EmailAddress,
                    LoanAmount = draftLoan.LoanAmount,
                    LoanTerm = draftLoan.Term,
                    TotalInterest = Math.Round(totalInterest, 2),
                    EstablishmentFee = product.EstablishmentFee,
                    RepaymentAmount = Math.Round(repaymentAmount, 2),
                    IsApplied = draftLoan.IsApplied
                });
        }
    }
}
