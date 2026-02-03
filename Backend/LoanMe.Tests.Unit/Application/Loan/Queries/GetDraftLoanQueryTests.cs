using LoanMe.Application.Loans.Queries;
using LoanMe.Shared.Models;
using LoanMe.Tests.Unit.Abstractions;

namespace LoanMe.Tests.Unit.Application.Loan.Queries {
    public class GetDraftLoanQueryTests : BaseUnitTest<GetDraftLoanQueryTests, GetDraftLoanQuery,
        Result<GetDraftLoanQueryResult>, GetDraftLoanQueryHandler> {
        [Fact]
        public void GetLoanQueryTests_Runs_Successfully() {
            Arrange(
                request => {
                    request.Id = 1;
                },
                expected => {
                    expected.Data = new() {
                        Title = "Mr.",
                        FirstName = "Steven",
                        LastName = "Tan",
                        DateOfBirth = new DateTime(1998, 06, 21),
                        MobileNumber = "12345678900",
                        EmailAddress = "steven@domain.com",
                        Term = 2,
                        LoanAmount = 5000m
                    };
                }
            )
            .Act()
            .Assert();
        }
    }
}
