using FluentAssertions;
using LoanMe.Application.Loans.Queries;
using LoanMe.Shared.Models;
using LoanMe.Tests.Unit.Abstractions;

namespace LoanMe.Tests.Unit.Application.Loan.Queries {
    public class CalculateQuoteQueryTests : BaseUnitTest<CalculateQuoteQueryTests, CalculateQuoteQuery, 
        Result<CalculateQuoteQueryResult>, CalculateQuoteQueryHandler> {
        [Fact]
        public void CalculateQuoteCommand_Runs_Successfully() {
            Arrange(
                request => {
                    request.ProductId = 1;
                    request.DraftLoanId = 1;
                },
                expected => {
                    expected.Successful = true;
                    expected.Data = new() {
                        TotalInterest = 500m,
                        EstablishmentFee = 300m,
                        RepaymentAmount = 123.45m
                    };
                }
            )
            .Act()
            .Assert(result => {
                result.Successful
                .Should()
                .BeTrue();

                result.Data
                .Should()
                .NotBeNull();
            });
        }
    }
}
