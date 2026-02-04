using FluentValidation;
using FluentValidation.Results;
using LoanMe.Application.Loans.Commands;
using LoanMe.Shared.Models;
using LoanMe.Tests.Unit.Abstractions;

namespace LoanMe.Tests.Unit.Application.Loan.Commands {
    public class ApplyLoanCommandTests : BaseUnitTestWithValidator<ApplyLoanCommandTests, ApplyLoanCommand, 
        Result<ApplyLoanCommandResult>, ApplyLoanCommandHandler, ApplyLoanCommandValidator> {
        [Fact]
        public void ApplyLoanCommand_Runs_Successfully() {
            Arrange(
                request => {
                    request.DraftLoanId = 1;
                    request.ProductId = 1;
                    request.RepaymentAmount = 1000m;
                    request.EstablishmentFee = 300m;
                    request.TotalInterest = 200m;
                },
                expected => {
                    expected.Data = new() {
                        ApplicationNumber = Guid
                            .NewGuid()
                            .ToString()
                    };
                }
            )
            .Act()
            .Assert();
        }

        [Fact]
        public void ApplyLoanCommand_Validate_Required_Parameters_Throws_Error() {
            Arrange(
                request => {
                    request.DraftLoanId = default;
                    request.ProductId = default;
                    request.RepaymentAmount = default;
                    request.EstablishmentFee = -1m;
                    request.TotalInterest = -2m;
                },
                validationResult => {
                    validationResult.Errors.Add(new ValidationFailure("DraftLoanId", "DraftLoanId is required and must be greater than zero."));
                    validationResult.Errors.Add(new ValidationFailure("ProductId", "ProductId is required and must be greater than zero."));
                    validationResult.Errors.Add(new ValidationFailure("RepaymentAmount", "Repayment amount is required and must be greater than zero."));
                    validationResult.Errors.Add(new ValidationFailure("EstablishmentFee", "Establishment fee must be greater than or equal to zero."));
                    validationResult.Errors.Add(new ValidationFailure("TotalInterest", "Total interest must be greater than or equal to zero."));
                }
            )
            .Act()
            .AssertThrows<ValidationException>();
        }
    }
}
